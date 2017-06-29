// Write your Javascript code.
$('.nav-tabs a').click(function (e) {
  e.preventDefault()
  $(this).tab('show')
})