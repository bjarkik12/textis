$(document).ready(function () {

    //$("#upvote-project").click(upvote(2));
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

//function ReloadPage() {
//    top.location.replace(updateURLParameter(document.location.href, "_rnd", Math.random()));
//    return false;
//}

function upvote(proj_id) {
    console.log("Upvote komið");
    //var tag1 = $(this);
    //var tag2 = tag1.prev().html();
    //var setId = "project" + tag2;
    //var par = tag1.parent().parent();
    //var par2 = par.children('project');
    //par2.attr('Id', setId);
    //$("#" + setId + " tr").remove();
    //console.log(tag1);
    //console.log(tag2);

    //var jObj = { "ProjectId": tag2 };
    var jObj = {};
    jObj = { id: proj_id };
    //alert(jObj.Name);

    $.ajax({
        type: "POST",
        url: "/Upvote/PostUpvote",
        data: jObj,
        dataType: "json",
        success: function (data) {
            window.location.reload();
            console.log(data);

            $("#upvoteTable").empty();
            //$("#upvoteTable").loadTemplate($("#upvoteTemplate"), data);

        },

        error: function (xhr, err) {
            console.log("Error creating Comment");
            // Note: just for debugging purposes!
            alert("readyState: " + xhr.readyState +
            "\nstatus: " + xhr.status);
            alert("responseText: " + xhr.responseText);
        }
    })
    //$.post("/Upvote/PostUpvote", jObj, function (data) {
    //    console.log("Posting upvote");
    //$.ajax({
    //    type: "GET",
    //    contentType: "application/json; charset=utf8",
    //    data: { 'ProjectId': tag2 },
    //    dataType: "json",
    //    success: function (data) {
    //        console.log("post succesful");
    //    },
    //    error: function (xhr, err) {
    //        console.log("post error");
    //    }

    //});

    event.preventDefault()

}

//$(document).ready(function () {

//    $('.dropdown_faq_header').each(function () {
//        var tis = $(this), state = false, answer = tis.next('.dropdown_faq_answer').slideUp();
//        tis.click(function () {
//            state = !state;
//            answer.slideToggle(state);
//            tis.toggleClass('active', state);
//            $("span", this).toggleClass("glyphicon glyphicon-chevron-up glyphicon glyphicon-chevron-down");
//        });
//    });

//    $("#Button").click(function () {
//        var comment = { "Text": $("#Text").val(), "ProjectId": $("#Id").val() };

//        //console.log(comment);
//        $.ajax({
//            type: "POST",
//            url: "/Comment/AddComment/",
//            data: comment,
//            dataType: "json",
//            success: function (data) {

//                console.log(data);
//                function ConvertStringToJSDate(dt) {
//                    var dtE = /^\/Date\((-?[0-9]+)\)\/$/.exec(dt);
//                    if (dtE) {
//                        var dt = new Date(parseInt(dtE[1], 10));
//                        return dt;
//                    }
//                    return null;
//                }
//                for (var i = 0; i < data.length; i++) {
//                    data[i].Date = ConvertStringToJSDate(data[i].Date);
//                }
//                $("#Text").val("");
//                $("#CommentUl").empty();
//                $("#CommentUl").loadTemplate($("#commentTemplate"), data);
//            },
//            error: function (xhr, err) {
//                console.log("Error creating Comment");
//                // Note: just for debugging purposes!
//            alert("readyState: " + xhr.readyState +
//            "\nstatus: " + xhr.status);
//            alert("responseText: " + xhr.responseText);
//        }
//        })
//    });

//        $("#upvote-project").click(function () {
//            console.log("Upvote komið");
//            //var tag1 = $(this);
//            //var tag2 = tag1.prev().html();
//            //var setId = "project" + tag2;
//            //var par = tag1.parent().parent();
//            //var par2 = par.children('project');
//            //par2.attr('Id', setId);
//            //$("#" + setId + " tr").remove();
//            //console.log(tag1);
//            //console.log(tag2);

//            //var jObj = { "ProjectId": tag2 };

//            var jObj = { "ProjectId": $("#Id").val() };

//            $.ajax({
//                type: "POST",
//                url: "/Upvote/PostUpvote",
//                data: jObj,
//                dataType: "json",
//                success: function (data) {

//                    console.log(data);

//                },
//                error: function (xhr, err) {
//                    console.log("Error creating Comment");
//                    // Note: just for debugging purposes!
//                    alert("readyState: " + xhr.readyState +
//                    "\nstatus: " + xhr.status);
//                    alert("responseText: " + xhr.responseText);
//                }
//            })
//            //$.post("/Upvote/PostUpvote", jObj, function (data) {
//            //    console.log("Posting upvote");
//            //$.ajax({
//            //    type: "GET",
//            //    contentType: "application/json; charset=utf8",
//            //    data: { 'ProjectId': tag2 },
//            //    dataType: "json",
//            //    success: function (data) {
//            //        console.log("post succesful");
//            //    },
//            //    error: function (xhr, err) {
//            //        console.log("post error");
//            //    }

//            //});
//        });
//        event.preventDefault();
//    });


////$("#link").click(function() {
////    alert("test");
////});

