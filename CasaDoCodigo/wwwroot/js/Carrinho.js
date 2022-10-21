class Carrinho {
    clickIncremento(btn) {
        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    getData(elemento) {

        let linhaDoItem = $(elemento).parents('[item-id]');
        let itemId = $(linhaDoItem).attr('item-id');
        let novaQtde = $(linhaDoItem).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQtde
        }

    }

    postQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;


        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (res) {
            let itemPedido = res.itemPedido;

            let linhaDoItem = $('[item-id=' + itemPedido.id + ']');

            linhaDoItem.find('input').val(itemPedido.quantidade);

            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());

            let carrinhoViewModel = res.carrinho;

            $('[numero-itens]').html(`Total: ${carrinhoViewModel.itens.length} itens`);

            $('[total]').html((carrinhoViewModel.total).duasCasas());

            if (itemPedido.quantidade == 0) {
                linhaDoItem.remove();
            }

        })
    }

    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }
}

var carrinho = new Carrinho();


Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}