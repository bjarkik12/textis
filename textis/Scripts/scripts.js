$(document).ready(function () {
    hide();
});

function hide() {
    var getting_started = document.getElementById("getting_started_index_text")
    getting_started.style.display = "none"

    var add_new_subtitle = document.getElementById("add_new_subtitle_index_text")
    add_new_subtitle.style.display = "none"

    var request_new_subtitle = document.getElementById("request_new_subtitle_index_text")
    request_new_subtitle.style.display = "none"
}

$('#click_getting_started').click(function () {
    $('#getting_started_index_text').toggle('1000');
    $("i", this).toggleClass("glyphicon glyphicon-chevron-up glyphicon glyphicon-chevron-down");
});

$('#click_add_new_subtitle').click(function () {
    $('#add_new_subtitle_index_text').toggle('1000');
    $("i", this).toggleClass("glyphicon glyphicon-chevron-up glyphicon glyphicon-chevron-down");
});

$('#click_request_new_subtitle').click(function () {
    $('#request_new_subtitle_index_text').toggle('1000');
    $("i", this).toggleClass("glyphicon glyphicon-chevron-up glyphicon glyphicon-chevron-down");
});

$('#faqs h3').each(function () {
    var tis = $(this), state = false, answer = tis.next('div').slideUp();
    tis.click(function () {
        state = !state;
        answer.slideToggle(state);
        tis.toggleClass('active', state);
    });
});