/* -------------------------------------------------------------------------------- /
	
	Magentech jQuery
	Created by Magentech
	v1.0 - 20.9.2016
	All rights reserved.
	
/ -------------------------------------------------------------------------------- */


	// Cart add remove functions
	var cart = {
        add: function (product_id, quantity, name, image) {
            addProduct(product_id, quantity);
            addProductNotice('Sản phẩm đã thêm vào giỏ hàng', "<img src='/Public/image/demo/" + image + "' alt=''>", '<h3><a href="#">' + name + '</a> added to <a href="#">shopping cart</a>!</h3>', 'success');
            
            
        },
        remove: function (product_id) {
           
            removeProduct(product_id);
            removeProductNotice('Sản phẩm đã xóa khỏi giỏ hàng', "", '<h3><a href="#">Product</a> remove <a href="#">shopping cart</a>!</h3>', 'success');
        }
	}
	

	

	/* ---------------------------------------------------
		jGrowl – jQuery alerts and message box
	-------------------------------------------------- */
	function addProductNotice(title, thumb, text, type) {
		$.jGrowl.defaults.closer = false;
		//Stop jGrowl
		//$.jGrowl.defaults.sticky = true;
		var tpl = thumb + "<h3>"+text+"</h3>";
		$.jGrowl(tpl, {		
			life: 4000,
			header: title,
			speed: 'slow',
			theme: type
		});
    }
    function removeProductNotice(title, thumb, text, type) {
        $.jGrowl.defaults.closer = false;
        //Stop jGrowl
        //$.jGrowl.defaults.sticky = true;
        var tpl = thumb + "<h3>" + text + "</h3>";
        $.jGrowl(tpl, {
            life: 4000,
            header: title,
            speed: 'slow',
            theme: type
        });
    }
    
    function addProduct(product_id,quantity) {
       
        var CartItem = [
            {
                ProductID: product_id,
                Quantity: quantity, //lấy giá trị hiện tại của textbox đó
                product: {
                    ProductID: product_id
                }
                
            }
        ];
        $.ajax({
            url: '/Cart/Add/',
            dataType: 'json',
            data: { cartModel: JSON.stringify(CartItem) }, //chuyển mảng thành một chuỗi,  
            type: 'POST',            
            success: function (response) {
                if (response.status == true) {
                    if (response.method == "add") {
                        
                        document.getElementById("cartnum").innerHTML = response.Count + ' sản phẩm';
                        document.getElementById("subtotal").innerHTML = response.subtotal;
                        var table = document.getElementById("tablesession");
                        $("#tablesession > tbody").append("<tr id='tr_" + response.productid+"'><td class='text-center' style='width: 70px'><a href='product.html'><img src='/Public/image/demo/" + response.image + "' style='width:70px' class='preview'></a></td>"
                            + "<td class='text-left'> <a class='cart_product_name' href='product.html'>" + response.name + "</a></td>"
                            + "<td id='quantity_" + response.productid + "' class='text-center'>" + response.quantity + "</td>"
                            + "<td class='text-center'> " + response.price + "</td >"
                            + "<td class='text-right'><a onclick='cart.remove(" + response.productid + ");' class='fa fa-times fa-delete'></a>'</td></tr> ");

                    }
                    else if (response.method == "exist") {
                        document.getElementById("cartnum").innerHTML = response.Count + ' sản phẩm';
                        document.getElementById("subtotal").innerHTML = response.subtotal;
                        document.getElementById("quantity_" + response.productid + "").innerText = response.quantity;
                        
                    }
                  
                }
            }

        })
    }   
    function removeProduct(product_id) {
            var row = document.getElementById("q_" + product_id);
            $.ajax({
                url: '/Cart/Delete/',
                dataType: 'json',
                data: { id: product_id },
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        if (response.Count == 0) {
                            window.location.reload();
                        }
                        document.getElementById("tr_" + response.productid).remove();
                        document.getElementById("cartnum").innerHTML = response.Count + ' sản phẩm';
                        document.getElementById("subtotal").innerHTML = response.subtotal.toString("N0");
                        document.getElementById("subtotol").innerText = response.subtotal.toString("N0");
                        row.remove();
                    }
                }
            });
    }   
       
	

