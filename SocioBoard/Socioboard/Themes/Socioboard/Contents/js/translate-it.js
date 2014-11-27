/* Translate-It Translation Tool
 * By Jon Raasch
 * http://jonraasch.com
 *
 * Copyright (c)2008 Jon Raasch. All rights reserved.
 * Released under FreeBSD License, see readme.txt
 * Do not remove the above copyright notice or text or 
 * the appended link to the author.
 *
 * For more information please visit: 
 * http://jonraasch.com/blog/translate-it-easy-translation-for-multiple-languages
*/

TranslateIt = {

    /**** config ****/
    
    // Edit usedLangs to modify languages.  Remove or add the first comment marks (//) to iclude or disclude languages.  You can also modify the order.  
    
    // Make sure that all array items end with a comma, except for the last one.
    usedLangs : [
        'ar',       // Arabic
        // 'bg',    // Bulgarian
        // 'ca',    // Catalan
        'zh-CN',       // Chinese
        // 'hr',    // Croatian
        // 'cs',    // Czech
        // 'da',    // Danish
        // 'nl',    // Dutch
        // 'en',    // English
        // 'tl',    // Filipino
        // 'fi',    // Finnish
        'fr',       // French
        'de',       // German
        // 'el',    // Greek
        // 'iw',    // Hebrew
        'hi',       // Hindi
        // 'id',    // Indonesian
        'it',       // Italian
        'ja',       // Japanese
        'ko',       // Korean
        // 'lv',    // Latvian
        // 'lt',    // Lithuanian
        // 'no',    // Norwegian
        // 'pl',    // Polish
        // 'pt',    // Portugese
        // 'ro',    // Romanian
        'ru',       // Russian
        // 'sr',    // Serbian
        // 'sk',    // Slovak
        // 'sl',    // Slovenian
        'es'        // Spanish
        // 'sv',    // Swedish
        // 'uk',    // Ukranian
        // 'vi',    // Vietnamese
    ],
    
    
    // bg image location - modify this if you want to host the bg image
    bgImage : 'http://farm4.static.flickr.com/3278/3074536325_7aacba09c6.jpg',
    
    // id of flags wrapper
    flagsWrap : 'translateFlags',
    
    // language you will be translating FROM (default is english)
    fromLang : 'en',
    
    // if you want to style the flags yourself, set this to 0
    useFlagCSS : 1,
    
    /**** end config ****/
    
    info : {
        'ar' : {
            'bgPos' : '0 0',
            'fullName' : 'Arabic'
        },
        'bg' : {
            'bgPos' : '-16px 0',
            'fullName' : 'Bulgarian'
        },
        'ca' : {
            'bgPos' : '-32px 0',
            'fullName' : 'Catalan'
        },
        'zh-CN' : {
            'bgPos' : '-64px 0',
            'fullName' : 'Chinese (Simplified)'
        },
        'hr' : {
            'bgPos' : '-80px 0',
            'fullName' : 'Croatian'
        },
        'cs' : {
            'bgPos' : '-96px 0',
            'fullName' : 'Czech'
        },
        'da' : {
            'bgPos' : '-112px 0',
            'fullName' : 'Danish'
        },
        'nl' : {
            'bgPos' : '-128px 0',
            'fullName' : 'Dutch'
        },
        'en' : {
            'bgPos' : '-144px 0',
            'fullName' : 'English'
        },
        'tl' : {
            'bgPos' : '-176px 0',
            'fullName' : 'Filipino'
        },
        'fi' : {
            'bgPos' : '-192px 0',
            'fullName' : 'Finnish'
        },
        'fr' : {
            'bgPos' : '-208px 0',
            'fullName' : 'French'
        },
        'de' : {
            'bgPos' : '-224px 0',
            'fullName' : 'German'
        },
        'el' : {
            'bgPos' : '-240px 0',
            'fullName' : 'Greek'
        },
        'iw' : {
            'bgPos' : '-256px 0',
            'fullName' : 'Hebrew'
        },
        'hi' : {
            'bgPos' : '-272px 0',
            'fullName' : 'Hindi'
        },
        'id' : {
            'bgPos' : '-288px 0',
            'fullName' : 'Indonesian'
        },
        'it' : {
            'bgPos' : '0 -11px',
            'fullName' : 'Italian'
        },
        'ja' : {
            'bgPos' : '-16px -11px',
            'fullName' : 'Japanese'
        },
        'ko' : {
            'bgPos' : '-32px -11px',
            'fullName' : 'Korean'
        },
        'lv' : {
            'bgPos' : '-48px -11px',
            'fullName' : 'Latvian'
        },
        'lt' : {
            'bgPos' : '-64px -11px',
            'fullName' : 'Lithuanian'
        },
        'no' : {
            'bgPos' : '-80px -11px',
            'fullName' : 'Norwegian'
        },
        'pl' : {
            'bgPos' : '-96px -11px',
            'fullName' : 'Polish'
        },
        'pt' : {
            'bgPos' : '-112px -11px',
            'fullName' : 'Portugese'
        },
        'ro' : {
            'bgPos' : '-128px -11px',
            'fullName' : 'Romanian'
        },
        'ru' : {
            'bgPos' : '-144px -11px',
            'fullName' : 'Russian'
        },
        'sr' : {
            'bgPos' : '-160px -11px',
            'fullName' : 'Serbian'
        },
        'sk' : {
            'bgPos' : '-176px -11px',
            'fullName' : 'Slovak'
        },
        'sl' : {
            'bgPos' : '-192px -11px',
            'fullName' : 'Slovenian'
        },
        'es' : {
            'bgPos' : '-208px -11px',
            'fullName' : 'Spanish'
        },
        'sv' : {
            'bgPos' : '-240px -11px',
            'fullName' : 'Swedish'
        },
        'uk' : {
            'bgPos' : '-256px -11px',
            'fullName' : 'Ukranian'
        },
        'vi' : {
            'bgPos' : '-272px -11px',
            'fullName' : 'Vietnamese'
        }
    },
    
    theFlags : [],

    addEvent : function(obj, evType, fn) {
        if (obj.addEventListener){
            obj.addEventListener(evType, fn, false);
            return true;
        } else if (obj.attachEvent){
            //MSIE method
            obj['e'+evType+fn] = fn;
            obj[evType+fn] = function() {
               obj['e'+evType+fn](window.event); 
            }
            obj.attachEvent("on"+evType, obj[evType+fn]);
            return true;
        } else {
            return false;
        }
    },

    translate : function ( toLang ) {
        var t = (( window.getSelection && window.getSelection() ) || ( document.getSelection && document.getSelection() ) || ( document.selection  && document.selection.createRange && document.selection.createRange().text ));
        
        var e = ( document.charset || document.characterSet );
        
        window.location = 'http://translate.google.com/translate' + ( t == '' ? '?u=' + escape(window.location.href) : '_t?text=' + t ) + '&hl=en&langpair=' + TranslateIt.fromLang + '|' + toLang + '&tbb=1&ie=' + e;
    },
    
    whichFlag : function( theFlag ) {
        for ( var i = 0; i < TranslateIt.theFlags.length; i++ ) {
            if ( theFlag == TranslateIt.theFlags[i] ) return i;
        }
    },
    
    buildFlags : function() {
        TranslateIt.flagsWrapObj = document.getElementById( TranslateIt.flagsWrap );
                
        for ( var i = 0; i < TranslateIt.usedLangs.length; i++ ) {
            thisLang = TranslateIt.usedLangs[i];
        
            var flag = TranslateIt.buildFlagNode( thisLang );
            
            
            TranslateIt.addEvent( flag, 'click', function(ev) {
                if ( typeof( ev.preventDefault ) != 'undefined' ) ev.preventDefault();
                
                var whichFlag = TranslateIt.whichFlag( ev.target || this );
                
                var newLang = TranslateIt.usedLangs[ whichFlag ];
                       
                TranslateIt.translate( newLang );
            });
            
            TranslateIt.flagsWrapObj.appendChild( flag );
            TranslateIt.theFlags.push( flag );
        }
        
        TranslateIt.appendAuthorInfo();
        TranslateIt.appendClear();
        
    },
    
    buildFlagNode : function( thisLang ) {
        flag = document.createElement('a');
        flag.href = '#';
        flag.innerHTML = thisLang;
        flag.className = 'translate-' + thisLang;
        flag.title = 'Translate into ' + TranslateIt.info[ thisLang ].fullName;
        
        if ( TranslateIt.useFlagCSS ) {
            flag.style.backgroundImage = 'url(' + TranslateIt.bgImage + ')';
            flag.style.backgroundPosition = TranslateIt.info[ thisLang ].bgPos;
            flag.style.display = 'block';
            flag.style.height = '11px';
            flag.style.width = '16px';
            flag.style.textIndent = '-1000px';
            flag.style.outline = 'none';
            flag.style.marginRight = '3px';
            flag.style.marginBottom = '3px';
            flag.style.overflow = 'hidden';
            flag.style.cssFloat = 'left';
            flag.style.styleFloat = 'left';//for ie
        }
        
        return flag;
    },
    
    // Please keep the link to the author, getting traffic on my blog is the reason I develop these free tools.  The link is built with Javascript so it will not effect SEO.
    appendAuthorInfo : function() {
        linkLove = document.createElement('a');
        linkLove.href = 'http://jonraasch.com/blog/translate-it-easy-translation-for-multiple-languages';
        linkLove.innerHTML = '(?)';
        linkLove.title = 'What is Translate-It?'
        
        linkLove.style.fontSize = '12px';
        linkLove.style.lineHeight = '12px';
        linkLove.style.textDecoration = 'none';
        linkLove.style.marginLeft = '5px';
        
        TranslateIt.flagsWrapObj.appendChild( linkLove );
    },
    
    appendClear : function() {
        theClear = document.createElement('br');
        theClear.style.clear = 'both';
        theClear.style.margin = '0';
        theClear.style.padding = '0';
        
        TranslateIt.flagsWrapObj.appendChild( theClear );
    },
    
    init : function( flagsWrap ) {
        var theLocation = window.location.href;
        
        if ( !theLocation.match(/\/translate_c\?hl\=/) ) {
            if ( typeof(flagsWrap) == 'string') TranslateIt.flagsWrap = flagsWrap;
            
            TranslateIt.addEvent( window, 'load', TranslateIt.buildFlags);
        }
    }
}