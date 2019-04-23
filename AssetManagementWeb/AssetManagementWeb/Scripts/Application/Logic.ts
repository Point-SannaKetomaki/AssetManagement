// <reference path="../typings/jquery/jquery.d.ts" />

/*Kopioitu AssetLocationModelista ja muokataan typescript muotoon
 * public class AssetLocationModel {
    public string AssetCode { get; set; }
        public string LocationCode { get; set; }
    }  ===>  */

class AssetLocationModel {
    public AssetCode: string;
    public LocationCode: string;
    }


function initAssetAssignment() {
    $("#AssignAssetButton").click(function () {
        //alert("Toimii!");

        var locationCode: string = $('#LocationCode').val();
        var assetCode: string = $('#AssetCode').val();

        alert('L: ' + locationCode + ', A: ' + assetCode);

        var data: AssetLocationModel = new AssetLocationModel();
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