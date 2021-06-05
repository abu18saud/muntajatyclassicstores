var _URL = window.URL || window.webkitURL;
$("#file").ready(function () {
    let onChange = function () {
        let _this = $(this)[0];

        let frmdata = new FromData();
        frmdata.append('File', _this.files[0]);

        $('#file').change(function () {
            readURL(this);
        });
        let onSucess = function (model) {
            $('.notification').html("<div class='alert alert - warning blue alert - dismissible' style='margin: 3px 20px 3px 20px' role='alert'>File uploaded succesfull</div>");

            $.ajax({
                method: 'POST',
                url: '/firebaseStorage/Index',
                data: frmdata,
                processData: false,
                contentType: false
            }).done(onSucess);

            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#displayImage').attr('src', e.target.result);
                    }

                    reader.readAsDataURL(input.files[0]);
                }
            }
            $(document).on('change', 'input[type = file]', onChange);
        }
    }

});

$("#file").change(function (e) {
    var file, img;
    console.log(this.files[0]);
    if ((file = this.files[0])) {
        var reader = new FileReader();

        //Read the contents of Image File.
        reader.readAsDataURL(file);
        reader.onload = function (e) {

            //Initiate the JavaScript Image object.
            var image = new Image();

            //Set the Base64 string return from FileReader as source.
            image.src = e.target.result;

            //Validate the File Height and Width.
            image.onload = function () {
                var height = this.height;
                var width = this.width;
                if (height > 600 || width > 1000) {
                    alert("عذراً ، الصورة المرفوعة من قِبلكم ليست موافقة لمعايير أحجام البنرات لدينا! \n يجب أن يكون عرض صورة البنر  بكسل1000 بينما طولها 600 بكسل");
                    $("#file").val(null);
                    return false;
                }
                //alert("Uploaded image has valid Height and Width.");
                return true;
            };
        };
    }
});


$('#confirmed').click(function (e) {
    console.log($("#totalWithVat").val());
    if (parseInt($("#totalWithVat")[0].innerHTML) != 0 && $("#firstName").val() != "" && $("#lastName").val() != "" && $("#_mobileNo").val() != "") {
        alert("تم الإرسال بنجاح");
        return true;
    } else {
        alert("إما أنّ الحقول غير معبئة أو أنّ قيمة مشترياتك أقل من 1 ر.س!!");
        return false;
    }
});

$('#confirmedAds').click(function (e) {
    //console.log($("#totalWithVat").val());
    if ($("#fullName").val() != "" && $("#mobileNo").val() != "" && $("#email").val() != "") {
        alert("تم الإرسال بنجاح");
        return true;
    } else {
        alert("يرجى تعبئة كافة الحقول فضلاً!!");
        return false;
    }
});


function hideMsg() {
    $("#messageCheckout").removeClass("show");
}

var A = document.getElementsByTagName("a");
for (var i = 0; i < A.length; i++) {
    if ($("#deliveryAccess")[0].innerHTML != "False") {
        //alert($("#deliveryAccess")[0].innerHTML);
        $("#delivery").prop('disabled', true);
    }
}



