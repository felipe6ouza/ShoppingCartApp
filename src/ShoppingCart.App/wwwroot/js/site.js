class Carrinho {
    clickIncremento(button) {
        let data = this.getData(button);
        data.Quantidade++;
        this.postQuantidade(data);

        if (data.Quantidade == 2) {
            $(button).parents('.quantidade-conjunto').children('.click-decremento').removeAttr("disabled");
        }

        $(button).parents('.quantidade-conjunto').children('.valor-quantidade').text(data.Quantidade);
        
    }

    clickDecremento(button) {
        let data = this.getData(button);
        
        if (data.Quantidade > 1) {
            data.Quantidade--;
            this.postQuantidade(data);

        }

        $(button).parents('.quantidade-conjunto').children('.valor-quantidade').text(data.Quantidade);

    }
    

    getData(elemento) {
        let quantidade = $(elemento).parents('.quantidade-conjunto').children('.valor-quantidade').text();
        let codigo = $(elemento).parents('.quantidade-conjunto').parent().parent().children('.produto-codigo').text();
        codigo = codigo.trim();

        return {
            Codigo: codigo,
            Quantidade: quantidade,
        };
    }

    postQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/Carrinho/AtualizarItemCarrinho',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),

        }).done(function () {
            let final = 0.0;
            let total = 0;
            let precos = $(".produto-preco");
            let quantidades = $('.valor-quantidade');


            for (let i = 0; i < precos.length; i++) {
                let a = precos[i].innerHTML;
                a = a.replace("R", "");
                a = a.replace("$", "");
                a = a.replace(",", ".");
                a = a.trim();
                a = parseFloat(a);

                let b = parseInt($(quantidades[i]).text());


                total += a * b;

                final = total;


            }

            let valorFormatado = final.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
            $("#valorTotal").text(valorFormatado);
        });
    }
}

var carrinho = new Carrinho();