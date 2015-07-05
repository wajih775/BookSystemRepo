jQuery(document).ready(function() {
    TimeAgo();
});

function TimeAgo() {
   jQuery("span[data-date]").each(function () {
        var span = jQuery(this);
        var time = span.attr("data-date");
        span.text(jQuery.timeago(time));
        
    });
   
}
