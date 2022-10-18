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
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data)
        }).done(function (res) {
            let itemPedido = res.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']');
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());
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