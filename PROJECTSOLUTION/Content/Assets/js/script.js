$("#landingModal").modal('show');

$("#jobsearch__searchformdiv").click(function(){
	$("#jobsearch__searchbox__primary").switchClass("jobsearch__searchbox__primary_col12","jobsearch__searchbox__primary_col6");
	$("#jobsearch__searchbox__secondry").addClass("displayblock",);
});

$(".jobsearchcatdiv > span").find("a").mouseover(function(e){
	$(e.target).addClass("underline");
});

$(".jobsearchcatdiv > span").find("a").mouseout(function(e){
	if ($(e.target) != $(".jobsearchcatdiv > span:nth-child(1)").html()){
	$(e.target).removeClass("underline");
}
});

$(".circle-plus").on('click', function(){
	var elem =$(this).parent().children()[2];
	$(elem).slideToggle("slow");
	$(this).toggleClass('opened');
});

$("#mobile_submit").click(function(e){
	e.preventDefault();
	$(".name-div").addClass("animated fadeInUp ")
	$(".name-div").css({"display":"block"})
	$(".otp-div").addClass("animated fadeInDown delay-1s ")
		$(".otp-div").css({"display":"block"})
	$(this).css({"display":"none"})
})


    $('.next').click(function(e){
    	e.preventDefault;
     $('.carousel').carousel('next');return false; });
    $('.prev').click(function(e){
    	e.preventDefault;
     $('.carousel').carousel('prev');return false; });


 $('.btnNext').click(function(){
  $('.nav-tabs > .active').next('a').trigger('click');
});

  $('.btnPrevious').click(function(){
  $('.nav-tabs > .active').prev('a').trigger('click');
});