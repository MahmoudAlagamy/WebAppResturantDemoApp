$(document).ready(function () {
    $("#Item").val(0);
    $("#Item").change(function () {
        var itemId = $("#Item").val();
        GetItemUnitPrice(itemId);
    });

    $("input[type = text]").change(function () {
        CalculateSubTotal();
    });

    $("input[type = text]").keyup(function () {
        CalculateBalance();
    });

    $("#btnAddToList").click(function () {
        AddToTheItemList();
    });

    $("#btnPayment").click(function () {
        FinalPayment();
    });

});

function FinalPayment() {
    var objOrderViewModel = {};
    var ListOfOrderDetailViewModel = new Array();
    //ListOfOrderDetailViewModel  OrderDetailViewModel
    objOrderViewModel.PaymentTypeId = $("#PaymentType").val();
    objOrderViewModel.CustomerId = $("#Customer").val();
    objOrderViewModel.FinalTotal = $("#txtFinalTotal").val();

    $("#tblResturantItemList").find("tr:gt(0)").each(function () {
        var OrderDetailViewModel = {};
        OrderDetailViewModel.Total = parseFloat($(this).find("td:eq(5)").text());
        OrderDetailViewModel.ItemId = parseFloat($(this).find("td:eq(0)").text());
        OrderDetailViewModel.UnitPrice = parseFloat($(this).find("td:eq(2)").text());
        OrderDetailViewModel.Quantity = parseFloat($(this).find("td:eq(3)").text());
        OrderDetailViewModel.Descount = parseFloat($(this).find("td:eq(4)").text());
        ListOfOrderDetailViewModel.push(OrderDetailViewModel);
    });

    objOrderViewModel.ListOfOrderDetailViewModel = ListOfOrderDetailViewModel;
    $.ajax({
        async: true,
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(objOrderViewModel),
        url: '/Home/Index',
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert("There Is A Problem To Adding The Data !.");
        }
    });
}

function CalculateBalance() {
    var FinalAmount = $("#txtPaymentTotal").val();
    var PaymentAmount = $("#txtPaymentAmount").val();
    var ReturnAmount = $("#txtReturnTotal").val();

    var BalanceAmount = parseFloat(FinalAmount) - parseFloat(PaymentAmount) + parseFloat(ReturnAmount);
    $("#txtBalance").val(parseFloat(BalanceAmount).toFixed(2))

    if (parseFloat(BalanceAmount) == 0) {
        $("#btnPayment").removeAttr("disabled")
    } else {
        $("#btnPayment").attr("disabled", "disabled")
    }
}

function RemoveItem(itemId) {
    $(itemId).closest('tr').remove();
}

function AddToTheItemList() {
    var tblItemList = $("#tblResturantItemList");
    var UnitPrice = $("#txtUnitPrice").val();
    var Quantity = $("#txtQuantity").val();
    var Descount = $("#txtDescount").val();
    var ItemId = $("#Item").val();
    var ItemName = $("#Item option:selected").text();

    var Total = (UnitPrice * Quantity) - Descount;

    var Itemlist =
        "<tr><td hidden>" +
        ItemId +
        "</td><td>" +
        ItemName +
        "</td><td>" +
        parseFloat(UnitPrice).toFixed(2) +
        "</td><td>" +
        parseFloat(Quantity).toFixed(2) +
        "</td><td>" +
        parseFloat(Descount).toFixed(2) +
        "</td><td>" +
        parseFloat(Total).toFixed(2) +
        "</td><td><input type='button' value='Remove' name='remove' class='btn btn-danger' onclick='RemoveItem(this)'/></tr>";
    tblItemList.append(Itemlist);
    FinalItemTotal();
    ResetItem();
}

function FinalItemTotal() {
    $("#txtFinalTotal").val("0.00");
    var FinalTotal = 0.00;
    $("#tblResturantItemList").find("tr:gt(0)").each(function () {
        var Total = parseFloat($(this).find("td:eq(5)").text());
        FinalTotal += Total;
    });

    $("#txtFinalTotal").val(parseFloat(FinalTotal).toFixed(2));
    $("#txtPaymentTotal").val(parseFloat(FinalTotal).toFixed(2));
    $("#txtBalance").val(parseFloat(FinalTotal).toFixed(2));
}

function ResetItem() {
    $("#txtUnitPrice").val('');
    $("#txtQuantity").val('');
    $("#txtDescount").val('0.00');
    $("#Item").val(0);
    $("#txtTotal").val('');
}

function CalculateSubTotal() {
    var UnitPrice = $("#txtUnitPrice").val();
    var Quantity = $("#txtQuantity").val();
    var Descount = $("#txtDescount").val();

    var Total = (UnitPrice * Quantity) - Descount;

    $("#txtTotal").val(parseFloat(Total).toFixed(2));
}

function GetItemUnitPrice(ItemId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: { ItemId: ItemId },
        url: '/Home/GetItemUnitPrice',
        success: function (data) {
            $("#txtUnitPrice").val(parseFloat(data).toFixed(2));
        },
        error: function () {
            alert("There Is A Problem To Get The Unit Price !.");
        }
    });
}