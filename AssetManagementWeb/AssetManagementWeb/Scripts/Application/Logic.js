// <reference path="../typings/jquery/jquery.d.ts" />
/*Kopioitu AssetLocationModelista ja muokataan typescript muotoon
 * public class AssetLocationModel {
    public string AssetCode { get; set; }
        public string LocationCode { get; set; }
    }  ===>  */
var AssetLocationModel = /** @class */ (function () {
    function AssetLocationModel() {
    }
    return AssetLocationModel;
}());
function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        //alert("Toimii!");
        var locationCode = $('#LocationCode').val();
        var assetCode = $('#AssetCode').val();
        alert('L: ' + locationCode + ', A: ' + assetCode);
        var data = new AssetLocationModel();
        data.LocationCode = locationCode;
        data.AssetCode = assetCode;
        //lähetetään json-muotoista dataa palvelimelle
        $.ajax({
            type: "POST",
            url: "/Asset/AssignLocation",
            data: JSON.stringify(data),
            contentType: "application/json",
            success: function (data) {
                if (data.success == true) {
                    alert("Asset succesfully assigned");
                }
                else {
                    alert("Error occurred: " + data.error);
                }
            },
            dataType: "json"
        });
    });
}
//# sourceMappingURL=Logic.js.map