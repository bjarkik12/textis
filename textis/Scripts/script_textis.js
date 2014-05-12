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
});

//$("#link").click(function() {
//    alert("test");
//});

