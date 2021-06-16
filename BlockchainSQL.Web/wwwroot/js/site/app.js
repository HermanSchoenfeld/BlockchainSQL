function Form_BeforeSubmit() {
    $(".contactForm :input").prop("disabled", true);
}

function Form_AfterSubmit_Success(result) {
    var alertType = result.Result ? "success" : "danger";
    var alertHeader = result.Result ? "Sent!" : "Apologies";
    $(".contactForm :input").prop("disabled", false);
    $("#contactFormResult").replaceWith('<br/><div class="form-result alert alert-' + alertType + '"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button><strong><i class="fa fa-check"></i> ' + alertHeader + '</strong> ' + result.Message + '</div>');
    if (result.Result) {
        $(".contactForm").get(0).reset();
    }
}

Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
}
// usage
//=> 3..padLeft() => '03'
//=> 3..padLeft(100,'-') => '--3' 

function GetBlockAge(block) {
    var a = new Date().getTime() / 1000;
    var e = block.TimeStampUnix;

    var d = a - e;
    if (d < 60) {
        return "< 1 minute";
    }
    if (d < 3600) {
        var c = (parseInt(d / 60) > 1) ? "s" : "";
        return parseInt(d / 60) + " minute" + c;
    }
    var c = (parseInt(d / 3600) > 1) ? "s" : "";
    return parseInt(d / 3600) + " hour" + c + " " + parseInt((d % 3600) / 60) + " minutes";
}



function FormatDateInternationalFormatLocal(d) {
    return [
        d.getFullYear(),
        (d.getMonth() + 1).padLeft(),
        d.getDate().padLeft()
    ].join('-') + ' ' +
        [
            d.getHours().padLeft(),
            d.getMinutes().padLeft(),
            d.getSeconds().padLeft()
        ].join(':');
}

function FormatDateInternationalFormatUTC(d) {
    return [
        d.getUTCFullYear(),
        (d.getUTCMonth() + 1).padLeft(),
        d.getUTCDate().padLeft()
    ].join('-') + ' ' +
        [
            d.getUTCHours().padLeft(),
            d.getUTCMinutes().padLeft(),
            d.getUTCSeconds().padLeft()
        ].join(':');
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}

function toAddressFormatFunction(output) {
    if (output.toAddressDisplay !== "coinbase") {
        let path = "/explorer/address?address=" + output.toAddressDisplay;
        return `<a href=${path}>${output.toAddressDisplay}</a>`
    }

    return output.toAddressDisplay;
}

function fromAddressFormatFunction(output) {
    if (output.fromAddressDisplay !== "coinbase") {
        let path = "/explorer/address?address=" + output.fromAddressDisplay;
        return `<a href=${path}>${output.fromAddressDisplay}</a>`
    }

    return output.fromAddressDisplay;
}