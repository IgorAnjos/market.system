// JQuery

//Declaração de variáveis alterar para o endereço do servidor
var endereco = "https://localhost:5001/Produto/Produto/";
var enderecoVenda = "https://localhost:5001/GerarVenda/GerarVenda/";

var produto;
var compra = [];
var _totalVenda = 0.0;

$("#posVenda").hide();

AtualizarTotal();


//Funções
function AtualizarTotal(){
    $("#totalVenda").html(_totalVenda);
}

function preencheForm(dadosProduto){ // recebo do backend
    $("#txtNome").val(dadosProduto.nome); // passando para os campos o valor 
    $("#txtCategoria").val(dadosProduto.categoria.nome);
    $("#txtFornecedor").val(dadosProduto.fornecedor.nome);
    $("#txtPrecoVenda").val(dadosProduto.precoVenda);
}

function ResetFormProduto(){
    $("#txtcodProduto").val("");
    $("#txtNome").val(""); 
    $("#txtCategoria").val("");
    $("#txtFornecedor").val("");
    $("#txtPrecoVenda").val("");
    $("#txtQuantidade").val("");
}

function addProduto(p,q){
    var produtoTemp = {};
    Object.assign(produtoTemp,produto);

    var venda = {produto: produtoTemp, quantidade: q, subtotal: produtoTemp.precoVenda * q};
    
    _totalVenda += venda.subtotal;
    AtualizarTotal();

    compra.push(produtoTemp);
    console.log(produtoTemp);
    $("#tbCompra").append(`<tr>
        <td>${p.id}</td>
        <td>${p.nome}</td>
        <td>${q}</td>
        <td>R$ ${p.precoVenda}</td>
        <td>${p.medicao}</td>
        <td>${p.precoVenda * q}</td>
        <td><button class='btn btn-danger'>Remover</button></td>
    </tr>`);
}

$("#formProduto").on("submit", function(event){
    event.preventDefault(); //impedir o carregamento 
    var produtoTabela = produto;
    var qntd = $("#txtQuantidade").val();
    //console.log(qntd);
    //console.log(produtoTabela);
    addProduto(produtoTabela,qntd);
    ResetFormProduto();
    //produto = undefined;
});

//Ajax
$("#pesquisar").click(function() {
    var codProdut = $("#txtcodProduto").val();
    var endecoTemp = endereco+codProdut;

    $.post(endecoTemp, function(dados, status){
        console.log(dados + status);
        produto = dados;

        var med = "";
        switch (produto.medicao){
            case 0:
                med = "Litro";
                break;
            case 1:
                med = "Kilo";
                break;
            case 2:
                med = "Unidade";
                break;
        }
        produto.medicao = med;
        preencheForm(produto);
        //console.log(produto);
    }).fail(function(){
        alert("Produto inválido");
    });
});

// Finalização de venda

$("#btnFinalizaVenda").click(function(){
    if(_totalVenda <= 0){
        alert("Compra Inválida nenhum produto adicionado");
        return;
    }

    var valorPago = $("#txtValorPago").val();
    if(!isNaN(valorPago))
    {
        if(valorPago >= _totalVenda)
        {
            console.log(valorPago);
            $("#posVenda").show();
            $("#preVenda").hide();
            $("#txtValorPago").prop("disabled", true);
            var valorTroco = valorPago - _totalVenda;
            $("#txtValorTroco").val(valorTroco);

            // processar array de compra
            compra.forEach(elemento => {
                elemento.produto = elemento. produto.id;
            });

            //Preparar um novo objeto
            var venda = {total: _totalVenda, troco: valorTroco, produtos: compra};

            // Enviar dados para o backend
            $.ajax({
                type:"POST",
                url: enderecoVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(venda),
                success: function (data){
                    console.log("Dados enviados");
                    console.log(data);
                }
            });

        }
        else
        {
            alert("Valor Pago é inferior ao valor da venda");
        }
    }
    else
    {
        alert("Valor Pago Inválido");
    }
});

function resetModal(){
    $("#posVenda").hide();
    $("#preVenda").show();
    $("#txtValorPago").prop("disabled", false);
    $("#txtValorPago").val("");
    $("#txtValorTroco").val("");
}

$("#btnFechar").click(function(){
    resetModal();
});