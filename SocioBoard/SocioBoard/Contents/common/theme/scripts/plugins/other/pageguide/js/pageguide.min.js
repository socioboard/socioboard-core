/*
 * Tracelytics PageGuide
 *
 * Copyright 2013 Tracelytics
 * Free to use under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 *
 * Contributing Author: Tracelytics Team
 */

/*
 * PageGuide usage:
 *
 *   Preferences:
 *     auto_show_first - Whether or not to focus on the first visible item
 *                       immediately on PG open (default true)
 *     loading_selector - The CSS selector for the loading element. pageguide
 *                        will wait until this element is no longer visible
 *                        starting up.
 *     track_events_cb - Optional callback for tracking user interactions
 *                       with pageguide.  Should be a method taking a single
 *                       parameter indicating the name of the interaction.
 *                       (default none)
 *     handle_doc_switch - Optional callback to enlight or adapt interface
 *                         depending on current documented element. Should be a
 *                         function taking 2 parameters, current and previous
 *                         data-tourtarget selectors. (default null)
 *     custom_open_button - Optional id for toggling pageguide. Default null.
 *                          If not specified then the default button is used.
 *     pg_caption - Optional - Sets the visible caption
 */
tl=window.tl||{};tl.pg=tl.pg||{};tl.pg.default_prefs={auto_show_first:!0,loading_selector:"#loading",track_events_cb:function(){},handle_doc_switch:null,custom_open_button:null,pg_caption:"page guide"};
tl.pg.init=function(a){if(0!==jQuery("#tlyPageGuide").length){var b=jQuery("#tlyPageGuide"),d=jQuery("<div>",{id:"tlyPageGuideWrapper"}),c=jQuery("<div>",{id:"tlyPageGuideMessages"});c.append('<a href="#" class="tlypageguide_close" title="Close Guide">close</a>').append("<span></span>").append("<div></div>").append('<a href="#" class="tlypageguide_back" title="Previous">Previous</a>').append('<a href="#" class="tlypageguide_fwd" title="Next">Next</a>');null==a.custom_open_button&&jQuery("<div/>",
{title:"Launch Page Guide","class":"tlypageguide_toggle"}).append(a.pg_caption).append("<div><span>"+b.data("tourtitle")+"</span></div>").append('<a href="javascript:void(0);" title="close guide">close guide &raquo;</a>').appendTo(d);d.append(b);d.append(c);jQuery("body").append(d);var e=new tl.pg.PageGuide(jQuery("#tlyPageGuideWrapper"),a);e.ready(function(){e.setup_handlers();e.$base.children(".tlypageguide_toggle").animate({right:"-120px"},250)});return e}};
tl.pg.PageGuide=function(a,b){this.preferences=jQuery.extend({},tl.pg.default_prefs,b);this.$base=a;this.$all_items=jQuery("#tlyPageGuide > li",this.$base);this.$items=jQuery([]);this.$message=jQuery("#tlyPageGuideMessages");this.$fwd=jQuery("a.tlypageguide_fwd",this.$base);this.$back=jQuery("a.tlypageguide_back",this.$base);this.cur_idx=0;this.track_event=this.preferences.track_events_cb;this.handle_doc_switch=this.preferences.handle_doc_switch;this.custom_open_button=this.preferences.custom_open_button};
tl.pg.isScrolledIntoView=function(a){var b=jQuery(window).scrollTop(),d=b+jQuery(window).height(),c=jQuery(a).offset().top;return c+jQuery(a).height()>=b&&c<=d-100};tl.pg.PageGuide.prototype.ready=function(a){var b=this,d=window.setInterval(function(){jQuery(b.preferences.loading_selector).is(":visible")||(a(),clearInterval(d))},250);return this};
tl.pg.PageGuide.prototype._on_expand=function(){var a=document,b=window;this.position_tour();this.cur_idx=0;var d=a.createElement("style");a.getElementsByTagName("head")[0].appendChild(d);b.createPopup||(d.appendChild(a.createTextNode("")),d.setAttribute("type","text/css"));var c=a.styleSheets[a.styleSheets.length-1],e="";this.$items.each(function(c){var f=jQuery(jQuery(this).data("tourtarget")+":visible:first");f.addClass("tlypageguide_shadow tlypageguide_shadow"+c);f=".tlypageguide_shadow"+c+":after { height: "+
f.outerHeight()+"px; width: "+f.outerWidth(!1)+"px; }";b.createPopup?e+=f:(f=a.createTextNode(f,0),d.appendChild(f));jQuery(this).prepend("<ins>"+(c+1)+"</ins>");jQuery(this).data("idx",c)});b.createPopup&&(c.cssText=e);this.preferences.auto_show_first&&0<this.$items.length&&this.show_message(0)};tl.pg.PageGuide.prototype.open=function(){this.track_event("PG.open");this._on_expand();this.$items.toggleClass("expanded");jQuery("body").addClass("tlypageguide-open")};
tl.pg.PageGuide.prototype.close=function(){this.track_event("PG.close");this.$items.toggleClass("expanded");this.$message.animate({height:"0"},500,function(){jQuery(this).hide()});jQuery("ins").remove();jQuery("body").removeClass("tlypageguide-open")};
tl.pg.PageGuide.prototype.setup_handlers=function(){var a=this;(null==a.custom_open_button?jQuery(".tlypageguide_toggle",this.$base):jQuery(a.custom_open_button)).live("click",function(){jQuery("body").is(".tlypageguide-open")?a.close():a.open();return!1});jQuery(".tlypageguide_close",this.$message).live("click",function(){a.close();return!1});this.$all_items.live("click",function(){var b=jQuery(this).data("idx");a.track_event("PG.specific_elt");a.show_message(b)});this.$fwd.live("click",function(){var b=
(a.cur_idx+1)%a.$items.length;a.track_event("PG.fwd");a.show_message(b);return!1});this.$back.live("click",function(){var b=(a.cur_idx+a.$items.length-1)%a.$items.length;a.track_event("PG.back");a.show_message(b,!0);return!1});jQuery(window).resize(function(){a.position_tour()})};
tl.pg.PageGuide.prototype.show_message=function(a,b){var d=this.$items[this.cur_idx],c=this.$items[a];this.cur_idx=a;this.handle_doc_switch&&this.handle_doc_switch(jQuery(c).data("tourtarget"),jQuery(d).data("tourtarget"));jQuery("div",this.$message).html(jQuery(c).children("div").html());this.$items.removeClass("tlypageguide-active");jQuery(c).addClass("tlypageguide-active");tl.pg.isScrolledIntoView(jQuery(c))||jQuery("html,body").animate({scrollTop:jQuery(c).offset().top-50},500);this.$message.not(":visible").show().animate({height:"100px"},
500);this.roll_number(jQuery("span",this.$message),jQuery(c).children("ins").html(),b)};tl.pg.PageGuide.prototype.roll_number=function(a,b,d){a.animate({"text-indent":(d?"":"-")+"50px"},"fast",function(){a.html(b);a.css({"text-indent":(d?"-":"")+"50px"},"fast").animate({"text-indent":"0"},"fast")})};
tl.pg.PageGuide.prototype.position_tour=function(){this.$items=this.$all_items.filter(function(){return jQuery(jQuery(this).data("tourtarget")).is(":visible")});this.$items.each(function(){var a=jQuery(this),b=jQuery(a.data("tourtarget")).filter(":visible:first"),d=b.offset().left,c=b.offset().top,c=a.hasClass("tlypageguide_top")?c-60:a.hasClass("tlypageguide_bottom")?c+(b.outerHeight()+15):c+5,d=a.hasClass("tlypageguide_right")?d+(b.outerWidth(!1)+15):a.hasClass("tlypageguide_left")?d-65:d+5;a.css({left:d+
"px",top:c+"px"})})};