jQuery(document).ready(function () {
    jQuery("#btnApplyNow").click(function () {
        //Save File Information
        jQuery.ajax({
            type: "POST",
            url: "../../Jobs/Apply",
            data: "&job_id="+jQuery("#btnApplyNow").attr("data-id"),
            beforeSend: function () {
                jQuery("#application-process .callout-description").text("");
                jQuery("#application-process .callout-description").append("<p><img src='../../content/images/loading.gif' style='width:25px; height:25px;' /> Processing Your Request...</p>");
                jQuery("#application-process").fadeIn("200");
            },
            success: function (d) {
                
                if (d.status == "success") {
                    jQuery("#application-process .callout-description").text("");
                    jQuery("#application-process .callout-description").append("<p style='color:green;'><i class='glyphicon glyphicon-ok'></i>Your job application has been submitted successfully.</p>");
                    jQuery("#application-process .callout").css("border", "2px solid rgb(1, 190, 126)");
                }

                if (d.status == "duplicate") {
                    jQuery("#application-process .callout-description").text("");
                    jQuery("#application-process .callout-description").append("<p style='color:rgb(192, 57, 57);'><i class='glyphicon glyphicon-exclamation-sign'></i>You have already applied for this job.</p>");
                    jQuery("#application-process .callout").css("border", "2px solid rgb(190, 1, 30)");
                }

                if (d.status == "expired") {
                    jQuery("#application-process .callout-description").text("");
                    jQuery("#application-process .callout-description").append("<p style='color:rgb(192, 57, 57);'><i class='glyphicon glyphicon-exclamation-sign'></i>This job is not available now.</p>");
                    jQuery("#application-process .callout").css("border", "2px solid rgb(190, 1, 30)");
                }

                if (d.status == "error") {
                    jQuery("#application-process .callout-description").text("");
                    jQuery("#application-process .callout-description").append("<p style='color:rgb(192, 57, 57);'><i class='glyphicon glyphicon-exclamation-sign'></i>There was an error. Please try again later (Error Code 0x00e1)</p>");
                    jQuery("#application-process .callout").css("border", "2px solid rgb(190, 1, 30)");
                }
            },
            error: function() {
                jQuery("#application-process .callout-description").text("");
                jQuery("#application-process .callout-description").append("<p style='color:rgb(192, 57, 57);'><i class='glyphicon glyphicon-exclamation-sign'></i>There was an error. Please try again later (Error Code 0x00g2).</p>");
                jQuery("#application-process .callout").css("border", "2px solid rgb(190, 1, 30)");
            }
        });
    });
});