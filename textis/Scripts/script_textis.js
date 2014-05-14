$(document).ready(function () {

    $('.dropdown_faq_header').each(function () {
        var tis = $(this), state = false, answer = tis.next('.dropdown_faq_answer').slideUp();
        tis.click(function () {
            state = !state;
            answer.slideToggle(state);
            tis.toggleClass('active', state);
            $("span", this).toggleClass("glyphicon glyphicon-chevron-up glyphicon glyphicon-chevron-down");
        });
    });

    $("#Button").click(function () {
        var comment = { "Text": $("#Text").val(), "ProjectId": $("#Id").val() };

        //console.log(comment);
        $.ajax({
            type: "POST",
            url: "/Comment/AddComment/",
            data: comment,
            dataType: "json",
            success: function (data) {
                
                console.log(data);
                function ConvertStringToJSDate(dt) {
                    var dtE = /^\/Date\((-?[0-9]+)\)\/$/.exec(dt);
                    if (dtE) {
                        var dt = new Date(parseInt(dtE[1], 10));
                        return dt;
                    }
                    return null;
                }
                for (var i = 0; i < data.length; i++) {
                    data[i].Date = ConvertStringToJSDate(data[i].Date);
                }
                $("#Text").val("");
                $("#CommentUl").empty();
                $("#CommentUl").loadTemplate($("#commentTemplate"), data);
            },
            error: function (xhr, err) {
                console.log("Error creating Comment");
                // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
        })

    });
});


//$("#link").click(function() {
//    alert("test");
//});

