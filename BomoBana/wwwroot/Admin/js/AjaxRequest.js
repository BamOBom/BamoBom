function Delete_Ajax(id, name, url) {
    swal({
        title: "واقعا میخوای پاک کنی ؟ 8 ثانیه وقت داری فکر کنی !",
        text: " در صورت حذف گزینه " + name + " امکان بازیابی وجود ندارد و تمام اطلاعات مربوط به این گزینه پاک میشود! ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "بله, حذف شود!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        timer: 8000,
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "POST",
                dataType: "JSON",
                data: { Id: id },
                async: true,
                cache: false ,
                success: function (data) {
                    $("#tr" + data.id).fadeOut();
                    swal("حذف شد!", "تمام اطلاعات مرتبط به گزینه " + data.name + " حذف شد", "success")
                    Notification_success('عملیات با موفقیت انجام شد');
                },
                error: function () {
                    Notification_error('عملیات با خطا روبرو شد');
                }
            });
        }
    });
};
function Failure_Ajax() {
    Notification_error('عملیات با خطا روبرو شد');
};

$("#Image").on('change', function () {
    //Get count of selected files
    var countFiles = $(this)[0].files.length;
    var imgPath = $(this)[0].value;
    var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
    var image_holder = $("#image-holder");
    image_holder.empty();

    if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
        if (typeof (FileReader) != "undefined") {

            //loop for each file selected for uploaded.
            for (var i = 0; i < countFiles; i++) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $("<img />", {
                        "src": e.target.result,
                        "class": "timg-fluid rounded",
                        "style": "width:100%"
                    }).appendTo(image_holder);
                }

                image_holder.show().fadeIn(500);
                reader.readAsDataURL($(this)[0].files[i]);
            }

        } else {
            alert("مشکلی در مرورگر شما وجود دارد");
        }
    } else {
        alert("فقط پسوندهای متداول تصاویر مورد قبول است.");
    }
});
function Update_Ajax(id, name, url) {
    swal({
        title: "تغییر وضعیت !",
        text: " از تغییر وضعیت گزینه " +  name  + " اطمینان دارید ؟ در صورت تائید باعث تغییر وضعیت گزینه های وابسته میشوید ! ",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "بله, تغییر کند!",
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "POST",
                dataType: "JSON",
                data: { Id: id },
                async: true,
                cache: false,
                success: function (data) {
                    console.log(data);
                    $("#tr" + data.id).addClass('update-item-tr');               
                    swal("گزینه " + data.isActivemsg + " شد! ", "وضعیت گزینه " + data.name + " تغییر کرد", "success")
                    Notification_success('عملیات با موفقیت انجام شد');
                    $("#IsActiveStatus" + data.id).empty();
                    if (data.isActive) {
                        $("#IsActiveStatus" + data.id).append('<span class="label label-success custom-label-acrive font-14 font-weight-100">' + data.isActivemsg + '<i class="ti-check text-active" aria-hidden="true"></i></span>');
                    }
                    else {
                        $("#IsActiveStatus" + data.id).append('<span class="label label-danger custom-label-acrive font-14 font-weight-100">' + data.isActivemsg + '<i class="fa fa-times" aria-hidden="true"></i></span>');
                        
                    }                  
                },
                error: function () {
                    Notification_error('عملیات با خطا روبرو شد');
                }
            });
        }
    });
};