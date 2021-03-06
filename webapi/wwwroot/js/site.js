﻿// Write your Javascript code.
$(document)
  .ajaxStart(function () {
    $('#wait-modal').modal('hide');
  })
  .ajaxStop(function () {
    $('#wait-modal').modal('hide');
  });

$(document).ready(function () {
  $('#wait-modal').modal('hide');
  $('.dropdown-toggle').dropdown();
});

$('.nav-tabs a').click(function (e) {
  e.preventDefault()
  $(this).tab('show')
})

$('.btn-input').click(function (e) {
  $('.btn-input').removeClass('active');
  $('.img-before.no-clicked').attr("src", "../../images/image-background-before-center.gif");
  $('.img-before.no-clicked').removeClass('disabled');
});

$('.img-before').click(function (e) {
  $img = $('.btn-input.active').find('img');
  if ($img.length == 1) {
    $(this).attr("src", $img.attr('src'));
    $(this).removeClass('no-clicked');
    $(this).addClass('clicked');
    $('.img-after.no-clicked').attr("src", "../../images/image-background-after-center.gif");
    $('.img-after.no-clicked').removeClass('disabled');
    $('#clean1').prop('disabled', false);
    $('#send1').prop('disabled', false);
  }
});

$('.img-after').click(function (e) {
  if (!$(this).hasClass('disabled')) {
    $img = $('.btn-input.active').find('img');
    $(this).attr("src", $img.attr('src'));
    $(this).removeClass('no-clicked');
    $(this).addClass('clicked');
    $('#clean2').prop('disabled', false);
    $('#send2').prop('disabled', false);
  }
});

$('#clean1').click(function (e) {
  $('.img-before').attr("src", "../../images/image-background-before-center.gif");
  $('.img-after').attr("src", "../../images/image-background.gif");
  $('.img-before').removeClass('clicked');
  $('.img-before').addClass('no-clicked');
  $('.img-after').removeClass('clicked');
  $('.img-after').addClass('no-clicked');
  $('.img-after').addClass('disabled');
  $('#clean1').prop('disabled', true);
  $('#send1').prop('disabled', true);
  $('#clean2').prop('disabled', true);
  $('#send2').prop('disabled', true);
});

$('#clean2').click(function (e) {
  $('.img-after.clicked').attr("src", "../../images/image-background-after-center.gif");
  $('.img-after').removeClass('clicked');
  $('.img-after').addClass('no-clicked');
  $('#clean2').prop('disabled', true);
  $('#send2').prop('disabled', true);
});

$('#send1').click(function (e) {
  
  $.ajax({
    method: "POST",
    url: "/api/Message",
    data: { Url: getImagesBefore()[0][0] }
  })
    .done(function( msg ) {
      alert( "Data Saved: " + msg );
    });
});

$('#send2').click(function (e) {

});

function getImagesBefore(){
  return getImages("img-before");
}
function getImagesAfter(){
  return getImages("img-after");
}
function getImages(img){
  var indexMax = 2;
  var result = new Array(indexMax+1);
  for (var i = 0; i<=indexMax; i++){
    result[i] = new Array(indexMax+1);
    for (var j = 0; j<=indexMax; j++){
      var tag = img + "-" + i + "-" + j;
      var $element = $("#"+ tag);
      result[i][j] = $element.attr('src');
    }
  }
  return result;
}