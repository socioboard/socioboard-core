/*
 * mopTip 2.2
 * By Hiroki Miura (http://www.mopstudio.jp)
 * Copyright (c) 2008 mopStudio
 * Licensed under the MIT License: http://www.opensource.org/licenses/mit-license.php
 * July 18, 2009
*/

var mopTipCount=0;
var startOpenMopTip=false;
var mopTipWindW,mopTipWindH,mopTipStyle;
var mopTipOpened=false;
var mopTipW,mopTipH;
var mopTipFunc,mopTipPin=false,mopTipContent;
var mopTipScrollX,mopTipScrollY,mopTipMouseX,mopTipMouseY;
var mopTipxOffset,mopTipyOffset;
var mopTipUa=navigator.userAgent,mopTipBrowser,mopTipIe=false;
var mopTipLeft,mopTipTop;
var mopTipHover=false;
var mopTipYoff;
var mopTipNoHeight=false;
var overClickCheck=false;
var closeBtnClick=false;
var mopTipCloseBtn=new Image();
var mopTipCloseBtnH=new Image();
var mopTipArrowBottom=new Image();
var mopTipArrowTop=new Image();
var mopTipBottom=new Image();
var mopTipLeft=new Image();
var mopTipLeftBottom=new Image();
var mopTipLeftTop=new Image();
var mopTipRight=new Image();
var mopTipRightBottom=new Image();
var mopTipRightTop=new Image();
var mopTipTop=new Image();
mopTipCloseBtn.src="../images/closeBtn.png"
mopTipCloseBtnH.src="../images/closeBtn_h.png"
mopTipArrowBottom.src="../images/arrowBottom.png"
mopTipArrowTop.src="../images/arrowTop.png"
mopTipBottom.src="../images/bottom.png"
mopTipLeft.src="../images/left.png"
mopTipLeftBottom.src="../images/leftBottom.png"
mopTipLeftTop.src="../images/leftTop.png"
mopTipRight.src="../images/right.png"
mopTipRightBottom.src="../images/rightBottom.png"
mopTipRightTop.src="../images/rightTop.png"
mopTipTop.src="../images/top.png"
/*line of changing shape from page bottom*/
var mopTipChangeY=200;
jQuery.fn.extend({
	mopTip:function(setting){
		$("#mopTip01").hide();
		$("#mopTip01 .close").hide();
		mopTipH=null;
		if($(this).attr("href")=="#"){
			$(this).attr({href:"#?moptip"});
		};
		$(this).css({cursor:"default"});
		$(this).mouseover(function(evt){
			$("#mopTip01 .close").hide(); 
			$("#mopTip01 .content").html("");
			if(mopTipCount==0){
				mopTipFunc.tipInit();
				mopTipCount+=1;
			};
			if(mopTipUa.indexOf("MSIE")>-1){
					mopTipIe=true;
			};
			mopTipW=setting.w;
			mopTipH=setting.h;
			mopTipStyle=setting.style;
			mopTipContent=$(setting.get).html();
			mopTipYoff=setting.yOff;
			
			if(mopTipH==null){
				mopTipNoHeight=true;
				if(mopTipYoff==null){
					mopTipYoff=-150;
				}
			}else{
				mopTipNoHeight=false;
			}
			$("#mopTip01 .tip").css({width:mopTipW+"px"});
			if(mopTipH!=null){
				$("#mopTip01 .tip").css({height:mopTipH+"px"});
			}else{
				mopTipH==null
				$("#mopTip01 .tip").css({height:"0px"});
			}
			$("#mopTip01 .content").html(mopTipContent);
			mopTipFunc.findPosi(evt);
			$("#mopTip01 table").hover(
				function(){mopTipHover==true;},
				function(){mopTipHover==false;}
			);
		});
		$(this).mouseout(function(){
			if(mopTipStyle=="overOut"){
				mopTipFunc.tipClose();
			}
			startOpenMopTip=false;
			mopTipStyle=false;
			mopTipPin=false;
		});
		$(this).click(function(){
			if(mopTipOpened==true){
				mopTipPin=true;
			}
		});
		$(this).mousemove(function(evt){
				$(".dForIe").hide();			   
				$(".dForIe").html(mopTipMouseX+mopTipxOffset);
				if((mopTipStyle!=false)&&(mopTipPin==false)){
					mopTipFunc.findPosi(evt);
				}
		});
		mopTipFunc={
			tipInit:function(){
				$("body").append(
					'<div class="dForIe"></div>'+
					'<div id="mopTip01">'+
					  '<table border="0" cellspacing="0" cellpadding="0">'+
						'<tr>'+
						  '<td class="leftTop">&nbsp;</td>'+
						  '<td><table class="arrowSet" border="0" cellspacing="0" cellpadding="0">'+
							'<tr>'+
							  '<td class="arrow">&nbsp;</td>'+
							  '<td class="top">&nbsp;</td>'+
							'</tr>'+
						  '</table></td>'+
						  '<td class="rightTop"> </td>'+
						'</tr>'+
						'<tr>'+
						  '<td class="left">&nbsp;</td>'+
						  '<td class="tip"><div class="content"></div></td>'+
						  '<td class="right">&nbsp;</td>'+
						'</tr>'+
						'<tr>'+
						  '<td class="leftBottom">&nbsp;</td>'+
						  '<td><table class="arrowSetBottom" border="0" cellspacing="0" cellpadding="0">'+
							'<tr>'+
							  '<td class="arrowBottom">&nbsp;</td>'+
							  '<td class="bottom">&nbsp;</td>'+
							'</tr>'+
						  '</table></td>'+
						  '<td class="rightBottom"></td>'+
						'</tr>'+
					  '</table>'+
					  '<div class="close">&nbsp;</div>'+
					'</div>'
				);
				/*close button click*/
				$("#mopTip01 .close").click(function(){
					closeBtnClick=true;	 
					mopTipFunc.tipClose();
				});
			},
			findPosi:function(evt){
				/*position*/
				if(mopTipIe==true){
					mopTipScrollX = document.documentElement.scrollLeft;
					mopTipScrollY = document.documentElement.scrollTop;
					mopTipMouseX=window.event.clientX+mopTipScrollX;
					mopTipMouseY=window.event.clientY+mopTipScrollY;
				}else{
					mopTipScrollX = window.pageXOffset;
					mopTipScrollY = window.pageYOffset;
					mopTipMouseX=evt.pageX;
					mopTipMouseY=evt.pageY;
				}
				mopTipWindW=document.documentElement.clientWidth;
				mopTipWindH=document.documentElement.clientHeight;
				var windXsc=mopTipWindW+mopTipScrollX;
				var windYsc=mopTipWindH+mopTipScrollY;
				var checkX=windXsc-mopTipMouseX;
				var checkY=windYsc-mopTipMouseY;
				
				var mopTipCheckHeight=mopTipChangeY;
				var preCheckHeight=mopTipH+40+40;

				
				if(mopTipNoHeight==false){
					if(mopTipCheckHeight<preCheckHeight){
						mopTipCheckHeight=preCheckHeight;
					}
				}else{
					mopTipCheckHeight=mopTipChangeY;
				}
				mopTipLeft=mopTipMouseX+mopTipxOffset;
				mopTipTop=mopTipMouseY+mopTipyOffset;
				if((checkX>mopTipW)&&(checkY>mopTipCheckHeight)){
					/*shape left top*/
					mopTipxOffset=-50;
					mopTipyOffset=30;
					$("#mopTip01 .arrowSet tr").html('<td class="arrow">&nbsp;</td><td class="top">&nbsp;</td>');
					$("#mopTip01 .top").css({width:mopTipW-60+"px",height:"20px"});
					$("#mopTip01 .arrowSet").css({width:mopTipW+"px"});
					$("#mopTip01 .arrowSetBottom tr").html('<td class="bottom">&nbsp;</td>');
					$("#mopTip01 .bottom").css({width:mopTipW+"px",height:"20px"});
				}else if((checkX<=mopTipW)&&(checkY>mopTipCheckHeight)){
					/*shape right top*/
					mopTipxOffset=-mopTipW-10;
					mopTipyOffset=30;
					$("#mopTip01 .arrowSet tr").html('<td class="top">&nbsp;</td><td class="arrow">&nbsp;</td>');
					$("#mopTip01 .top").css({width:mopTipW-60+"px",height:"20px"});
					$("#mopTip01 .arrowSet").css({width:mopTipW+"px"});
					$("#mopTip01 .arrowSetBottom tr").html('<td class="bottom">&nbsp;</td>');
					$("#mopTip01 .bottom").css({width:mopTipW+"px",height:"20px"});
				}else if((checkX>mopTipW)&&(checkY<=mopTipCheckHeight)){
					/*shape left bottom*/
					mopTipxOffset=-50;
					if(mopTipNoHeight==false){
						mopTipyOffset=-(mopTipH+40+40);
					}else{
						mopTipyOffset=mopTipYoff;
					}
					$("#mopTip01 .arrowSet tr").html('<td class="top"></td>');
					$("#mopTip01 .top").css({width:mopTipW+"px",height:"20px"});
					$("#mopTip01 .arrowSet").css({width:mopTipW+"px"});
					$("#mopTip01 .arrowSetBottom tr").html('<td class="arrowBottom">&nbsp;</td><td class="bottom">&nbsp;</td>');
					$("#mopTip01 .bottom").css({width:mopTipW-60+"px",height:"20px"});
				}else if((checkX<=mopTipW)&&(checkY<=mopTipCheckHeight)){
					/*shape right bottom*/
					mopTipxOffset=-mopTipW-10;
					if(mopTipNoHeight==false){
						mopTipyOffset=-(mopTipH+40+40);
					}else{
						mopTipyOffset=mopTipYoff;
					}
					$("#mopTip01 .arrowSet tr").html('<td class="top"></td>');
					$("#mopTip01 .top").css({width:mopTipW+"px",height:"20px"});
					$("#mopTip01 .arrowSet").css({width:mopTipW+"px"});
					$("#mopTip01 .arrowSetBottom tr").html('<td class="bottom">&nbsp;</td><td class="arrowBottom">&nbsp;</td>');
					$("#mopTip01 .bottom").css({width:mopTipW-60+"px",height:"20px"});
				}
				/*pngFix*/
				$("#mopTip01").pngFix();
				mopTipFunc.setPosi();
			},
			setPosi:function(){
				$("#mopTip01").css({
					position: "absolute",
					width:mopTipW+40+"px",
					left:mopTipMouseX+mopTipxOffset+"px",
					top:mopTipMouseY+mopTipyOffset+"px"
				});
				mopTipFunc.openTimeout();
			},
			openTimeout:function(){
				startOpenMopTip=true;
				if(mopTipStyle!="overOut"){
					$("#mopTip01 .close").show();
				}
				setTimeout("mopTipFunc.tipOpen()",500);
			},
			tipOpen:function(){
				if(startOpenMopTip==true){
					/*put content*/
					$("#mopTip01").show();
					mopTipOpened=true;
					closeBtnClick=false;
				}
			},
			tipClose:function(){
				startOpenMopTip=false;
				mopTipOpened=false;
				mopTipH=null;
				setTimeout("mopTipFunc.tipClosing()",150);
			},
			tipClosing:function(){
				$("#mopTip01 table").focus();
				$("#mopTip01 .close").hide();
				$("#mopTip01").hide();
			} 
		}
	}
});
