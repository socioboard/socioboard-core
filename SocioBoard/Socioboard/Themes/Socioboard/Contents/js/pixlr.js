var pixlr = (function () {
    /*
     * IE only, size the size is only used when needed
     */
    function windowSize() {
        var w = 0,
            h = 0;
        if (document.documentElement.clientWidth !== 0) {
            w = document.documentElement.clientWidth;
            h = document.documentElement.clientHeight;
        } else {
            w = document.body.clientWidth;
            h = document.body.clientHeight;
        }
        return {
            width: w,
            height: h
        };
    }

    function extend(object, extender) {
        for (var attr in extender) {
            if (extender.hasOwnProperty(attr)) {
                object[attr] = extender[attr] || object[attr];
            }
        }
        return object;
    }

    function buildUrl(opt) {
        var url = 'http://pixlr.com/' + opt.service + '/?s=c', attr;
        for (attr in opt) {
            if (opt.hasOwnProperty(attr) && attr !== 'service') {
                url += "&" + attr + "=" + escape(opt[attr]);
            }
        }
        return url;
    }
    var bo = {
        ie: window.ActiveXObject,
        ie6: window.ActiveXObject && (document.implementation !== null) && (document.implementation.hasFeature !== null) && (window.XMLHttpRequest === null),
        quirks: document.compatMode === 'BackCompat' },
        return_obj = {
            settings: {
                'service': 'editor'
            },
            overlay: {
                show: function (options) { 
                    var opt = extend(return_obj.settings, options || {}),
                        iframe = document.createElement('iframe'),
                        div = pixlr.overlay.div = document.createElement('div'),
                        but = pixlr.overlay.but = document.createElement('button'),
                        idiv = pixlr.overlay.idiv = document.createElement('div');
                   

                    div.style.background = '#696969';
                    div.style.opacity = 0.8;
                    div.style.filter = 'alpha(opacity=80)';
                    div.id = 'overlay_div';
                    idiv.id = 'iframe_div';
                    idiv.style.position = 'relative';
                    
                   
                    var t = document.createTextNode("X");
                    but.appendChild(t); 

                   
                    
                    but.style.width = '20px';
                    but.style.top = '-18px';
                    but.style.right = '-4px';
                    but.style.position = 'absolute';
                    but.style.border = '1px solid red';
                    but.style.borderRadius = '50%';
                    but.style.backgroundColor = 'red';
                    but.style.color = 'white';
                    
                   
                  
                    
                    but.onclick = function() {
                    	$(document).find("#overlay_div").remove();
                    	$(document).find("#iframe_div").remove();
                    };
                    
                    

                    if ((bo.ie && bo.quirks) || bo.ie6) {
                        var size = windowSize();
                        div.style.position = 'absolute';
                        div.style.width = size.width + 'px';
                        div.style.height = size.height + 'px';
                        div.style.setExpression('top', "(t=document.documentElement.scrollTop||document.body.scrollTop)+'px'");
                        div.style.setExpression('left', "(l=document.documentElement.scrollLeft||document.body.scrollLeft)+'px'");
                    } else {
                        div.style.width = '100%';
                        div.style.height = '100%';
                        div.style.top = '0';
                        div.style.left = '0';
                        div.style.position = 'fixed';
                    }
                    div.style.zIndex = 99998;

                    idiv.style.border = '1px solid #2c2c2c';
                    if ((bo.ie && bo.quirks) || bo.ie6) {
                        idiv.style.position = 'absolute';
                        idiv.style.setExpression('top', "25+((t=document.documentElement.scrollTop||document.body.scrollTop))+'px'");
                        idiv.style.setExpression('left', "35+((l=document.documentElement.scrollLeft||document.body.scrollLeft))+'px'");
                    } else {
                        idiv.style.position = 'fixed';
                        idiv.style.top = '25px';
                        idiv.style.left = '35px';
                    }
                    idiv.style.zIndex = 99999;

                    document.body.appendChild(div);
                    document.body.appendChild(idiv);
                    
                    
                    
                    
                    
                    //document.body.appendChild("<button>Remove</button>");

                    iframe.style.width = (div.offsetWidth - 70) + 'px';
                    iframe.style.height = (div.offsetHeight - 50) + 'px';
                    iframe.style.border = '1px solid #b1b1b1';
                    iframe.style.backgroundColor = '#606060';
                    iframe.style.display = 'block';
                    iframe.frameBorder = 0;
                    iframe.src = buildUrl(opt);
                    idiv.appendChild(but);
                    idiv.appendChild(iframe);
                },
                hide: function (callback) {
                	
                    if (pixlr.overlay.idiv && pixlr.overlay.div) {
                        document.body.removeChild(pixlr.overlay.idiv);
                        document.body.removeChild(pixlr.overlay.div);
                    }
                    if (callback) {
                        eval(callback);
                    }
                }
            },
            url: function(options) {
           
                return buildUrl(extend(return_obj.settings, options || {}));
            },
            edit: function (options) {
            	
                var opt = extend(return_obj.settings, options || {});
                location.href = buildUrl(opt);
            }
        };  
    return return_obj;
}());