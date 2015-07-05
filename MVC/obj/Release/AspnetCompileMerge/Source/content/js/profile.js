jQuery(document).ready(function() {
    
    jQuery('.popup-modal').magnificPopup({
        type: 'inline',
        preloader: false,
        modal: true
    });

    //Upload Profile Photo
    jQuery(function () {
        jQuery('#fileupload').fileupload({
            dataType: 'json',
            add: function (e, data) {
                jQuery(".user-dp-upload").css("display", "none"); //fix: hide menu

                if (data.files.length > 0) {
                    var fname = data.files[0].name;
                    var ext = fname.split('.').pop().toLowerCase();
                    if (jQuery.inArray(ext, ['bmp', 'png', 'jpg', 'jpeg']) == -1) {
                        alert('Invalid File, Allowed Files are BMP/JPG/JPEG/PNG');
                    } else {
                        data.submit();
                    }
                    
                }
                
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                jQuery('#progress').css("display", "block");
                jQuery('#progress .count').text(progress + '%');


            },
            done: function (e, data) {
                jQuery.each(data.result.files, function (index, file) {
                    
                    //Save File Information
                    jQuery.ajax({
                        type: "POST",
                        url: "../../Profile/UpdateUserPhoto",
                        data: "&fileName=" + file.SavedFileName,
                        success: function (d) {
                            
                            if (d.status == "success") {
                                
                                jQuery("#user-photo").attr("src", "../../content/static/"+file.SavedFileName);
                            }
                        }
                    });


                });
            },
            always: function(e, data) {
                jQuery('#progress').css("display", "none");
            }
        });
    });

    //Upload CV
    jQuery(function () {
        jQuery('#cvUpload').fileupload({
            dataType: 'json',
            add: function (e, data) {
                if (data.files.length > 0) {
                    var fname = data.files[0].name;
                    var ext = fname.split('.').pop().toLowerCase();
                    if (jQuery.inArray(ext, ['doc', 'docx', 'pdf']) == -1) {
                        alert('Invalid File, Allowed Files are doc/docx/pdf');
                    } else {
                        data.submit();
                    }

                }

            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                jQuery('#cvProgress').css("display", "block");
                jQuery('#cvProgress .progress-bar-status').css("width",progress + '%');


            },
            done: function (e, data) {
                jQuery.each(data.result.files, function (index, file) {

                    //Save File Information
                    jQuery.ajax({
                        type: "POST",
                        url: "../../Profile/UpdateUserCV",
                        data: "&fileName=" + file.SavedFileName,
                        success: function (d) {

                            if (d.status == "success") {

                                jQuery("#download-cv").attr("href", "../../content/static/" + file.SavedFileName);
                            }
                        }
                    });


                });
            },
            always: function (e, data) {
                jQuery('#cvProgress').css("display", "none");
            }
        });
    });

    jQuery("#user-display-image").mouseenter(function () {
        jQuery(".user-dp-upload").css("display", "block");
    });

    jQuery("#user-display-image").mouseleave(function () {
        jQuery(".user-dp-upload").css("display", "none");
    });

   

    

});
