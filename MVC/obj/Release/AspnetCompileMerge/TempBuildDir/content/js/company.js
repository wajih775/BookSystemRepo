jQuery(document).ready(function() {
    

    //Upload CV
    jQuery(function () {
        jQuery('#logoUpload').fileupload({
            dataType: 'json',
            add: function (e, data) {
                if (data.files.length > 0) {
                    var fname = data.files[0].name;
                    var ext = fname.split('.').pop().toLowerCase();
                    if (jQuery.inArray(ext, ['bmp', 'png', 'jpg', 'jpeg']) == -1) {
                        alert('Invalid File, Allowed Files are BMP/PNG/JPG/JPED');
                    } else {
                        data.submit();
                    }

                }

            },
            progressall: function (e, data) {
                
                var progress = parseInt(data.loaded / data.total * 100, 10);
                jQuery('#logoProgress').css("display", "block");
                jQuery('#logoProgress .progress-bar-status').text("Uploading: "+progress + '%');
                

            },
            done: function (e, data) {
                jQuery.each(data.result.files, function (index, file) {

                    jQuery("#Logo").val(file.SavedFileName);
                    jQuery("#company-logo").attr("src", "../../content/static/" + file.SavedFileName);
                });
            },
            always: function (e, data) {
                jQuery('#logoProgress').css("display", "none");
            }
        });
    });

   
    

});
