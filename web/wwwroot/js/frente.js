// JQuery

//Declaração de variáveis
var endereco = "https://localhost:5001/Produto/Produto/";
var produto;
var compra = [];

//Funções
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
    compra.push(produtoTemp);
    
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
    console.log(qntd);
    console.log(produtoTabela);
    addProduto(produtoTabela,qntd);
    ResetFormProduto();
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
        console.log(produto);
    }).fail(function(){
        alert("Produto inválido");
    });
});