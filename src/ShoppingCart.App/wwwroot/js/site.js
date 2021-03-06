﻿class Carrinho {
    clickIncremento(button) {
        let data = this.getData(button);
        data.Quantidade++;
        this.postQuantidade(data);

        if (data.Quantidade == 2) {
            $(button).parents('.quantidade-conjunto').children('.input-group-btn').children('.click-decremento').attr("disabled", false);
        }

        $(button).parents('.quantidade-conjunto').children('.valor-quantidade').text(data.Quantidade);
        
    }

    clickDecremento(button) {
        let data = this.getData(button);


        if (data.Quantidade > 1) {
            data.Quantidade--;
            this.postQuantidade(data);

            if (data.Quantidade == 1) {
                $(button).attr("disabled", true);

            }
        }

        $(button).parents('.quantidade-conjunto').children('.valor-quantidade').text(data.Quantidade);

    }
    

    getData(elemento) {
        let quantidade = $(elemento).parents('.quantidade-conjunto').children('.valor-quantidade').text();
        let nome = $(elemento).parents('.quantidade-conjunto').parent().parent().children('.produto-nome').text();
        nome = nome.trim();



        return {
            Nome: nome,
            Quantidade: quantidade,
        };
    }

    postQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/Carrinho/AtualizarQuantidade',
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

