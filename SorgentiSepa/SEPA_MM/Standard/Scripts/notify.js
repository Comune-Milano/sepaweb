(function (factory) {
    // UMD start

    if (typeof define === 'function' && define.amd) {
        // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof module === 'object' && module.exports) {
        // Node/CommonJS
        module.exports = function (root, jQuery) {
            if (jQuery === undefined) {
                // require('jQuery') returns a factory that requires window to
                // build a jQuery instance, we normalize how we use modules
                // that require this pattern but the window provided is a noop
                // if it's defined (how jquery works)
                if (typeof window !== 'undefined') {
                    jQuery = require('jquery');
                }
                else {
                    jQuery = require('jquery')(root);
                }
            }
            factory(jQuery);
            return jQuery;
        };
    } else {
        // Browser globals
        factory(jQuery);
    }
} (function ($) {
    //IE8 indexOf polyfill
    var indexOf = [].indexOf || function (item) {
        for (var i = 0, l = this.length; i < l; i++) {
            if (i in this && this[i] === item) {
                return i;
            }
        }
        return -1;
    };

    var pluginName = "notify";
    var pluginClassName = pluginName + "js";
    var blankFieldName = pluginName + "!blank";

    var positions = {
        t: "top",
        m: "middle",
        b: "bottom",
        l: "left",
        c: "center",
        r: "right"
    };
    var hAligns = ["l", "c", "r"];
    var vAligns = ["t", "m", "b"];
    var mainPositions = ["t", "b", "l", "r"];
    var opposites = {
        t: "b",
        m: null,
        b: "t",
        l: "r",
        c: null,
        r: "l"
    };

    var parsePosition = function (str) {
        var pos;
        pos = [];
        $.each(str.split(/\W+/), function (i, word) {
            var w;
            w = word.toLowerCase().charAt(0);
            if (positions[w]) {
                return pos.push(w);
            }
        });
        return pos;
    };

    var styles = {};

    var coreStyle = {
        name: "core",
        html: "<div class=\"" + pluginClassName + "-wrapper\">\n	<div class=\"" + pluginClassName + "-arrow\"></div>\n	<div class=\"" + pluginClassName + "-container\"></div>\n</div>",
        css: "." + pluginClassName + "-corner {\n	position: fixed;\n	margin: 5px;\n	z-index: 1050;\n}\n\n." + pluginClassName + "-corner ." + pluginClassName + "-wrapper,\n." + pluginClassName + "-corner ." + pluginClassName + "-container {\n	position: relative;\n	display: block;\n	height: inherit;\n	width: inherit;\n	margin: 3px;\n}\n\n." + pluginClassName + "-wrapper {\n	z-index: 1;\n	position: absolute;\n	display: inline-block;\n	height: 0;\n	width: 0;\n}\n\n." + pluginClassName + "-container {\n	display: none;\n	z-index: 1;\n	position: absolute;\n}\n\n." + pluginClassName + "-hidable {\n	cursor: pointer;\n}\n\n[data-notify-text],[data-notify-html] {\n	position: relative;\n}\n\n." + pluginClassName + "-arrow {\n	position: absolute;\n	z-index: 2;\n	width: 0;\n	height: 0;\n}"
    };

    var stylePrefixes = {
        "border-radius": ["-webkit-", "-moz-"]
    };

    var getStyle = function (name) {
        return styles[name];
    };

    var addStyle = function (name, def) {
        if (!name) {
            throw "Missing Style name";
        }
        if (!def) {
            throw "Missing Style definition";
        }
        if (!def.html) {
            throw "Missing Style HTML";
        }
        //remove existing style
        var existing = styles[name];
        if (existing && existing.cssElem) {
            if (window.console) {
                console.warn(pluginName + ": overwriting style '" + name + "'");
            }
            styles[name].cssElem.remove();
        }
        def.name = name;
        styles[name] = def;
        var cssText = "";
        if (def.classes) {
            $.each(def.classes, function (className, props) {
                cssText += "." + pluginClassName + "-" + def.name + "-" + className + " {\n";
                $.each(props, function (name, val) {
                    if (stylePrefixes[name]) {
                        $.each(stylePrefixes[name], function (i, prefix) {
                            return cssText += "	" + prefix + name + ": " + val + ";\n";
                        });
                    }
                    return cssText += "	" + name + ": " + val + ";\n";
                });
                return cssText += "}\n";
            });
        }
        if (def.css) {
            cssText += "/* styles for " + def.name + " */\n" + def.css;
        }
        if (cssText) {
            def.cssElem = insertCSS(cssText);
            def.cssElem.attr("id", "notify-" + def.name);
        }
        var fields = {};
        var elem = $(def.html);
        findFields("html", elem, fields);
        findFields("text", elem, fields);
        def.fields = fields;
    };

    var insertCSS = function (cssText) {
        var e, elem, error;
        elem = createElem("style");
        elem.attr("type", 'text/css');
        $("head").append(elem);
        try {
            elem.html(cssText);
        } catch (_) {
            elem[0].styleSheet.cssText = cssText;
        }
        return elem;
    };

    var findFields = function (type, elem, fields) {
        var attr;
        if (type !== "html") {
            type = "text";
        }
        attr = "data-notify-" + type;
        return find(elem, "[" + attr + "]").each(function () {
            var name;
            name = $(this).attr(attr);
            if (!name) {
                name = blankFieldName;
            }
            fields[name] = type;
        });
    };

    var find = function (elem, selector) {
        if (elem.is(selector)) {
            return elem;
        } else {
            return elem.find(selector);
        }
    };

    var pluginOptions = {
        clickToHide: true,
        autoHide: true,
        autoHideDelay: 3000,
        arrowShow: true,
        arrowSize: 5,
        breakNewLines: true,
        elementPosition: "bottom",
        globalPosition: "top left",
        style: "bootstrap",
        className: "error",
        showAnimation: "slideDown",
        showDuration: 400,
        hideAnimation: "slideUp",
        hideDuration: 200,
        gap: 20
    };

    var inherit = function (a, b) {
        var F;
        F = function () { };
        F.prototype = a;
        return $.extend(true, new F(), b);
    };

    var defaults = function (opts) {
        return $.extend(pluginOptions, opts);
    };

    var createElem = function (tag) {
        return $("<" + tag + "></" + tag + ">");
    };

    var globalAnchors = {};

    var getAnchorElement = function (element) {
        var radios;
        if (element.is('[type=radio]')) {
            radios = element.parents('form:first').find('[type=radio]').filter(function (i, e) {
                return $(e).attr("name") === element.attr("name");
            });
            element = radios.first();
        }
        return element;
    };

    var incr = function (obj, pos, val) {
        var opp, temp;
        if (typeof val === "string") {
            val = parseInt(val, 10);
        } else if (typeof val !== "number") {
            return;
        }
        if (isNaN(val)) {
            return;
        }
        opp = positions[opposites[pos.charAt(0)]];
        temp = pos;
        if (obj[opp] !== undefined) {
            pos = positions[opp.charAt(0)];
            val = -val;
        }
        if (obj[pos] === undefined) {
            obj[pos] = val;
        } else {
            obj[pos] += val;
        }
        return null;
    };

    var realign = function (alignment, inner, outer) {
        if (alignment === "l" || alignment === "t") {
            return 0;
        } else if (alignment === "c" || alignment === "m") {
            return outer / 2 - inner / 2;
        } else if (alignment === "r" || alignment === "b") {
            return outer - inner;
        }
        throw "Invalid alignment";
    };

    var encode = function (text) {
        encode.e = encode.e || createElem("div");
        return encode.e.text(text).html();
    };

    function Notification(elem, data, options) {
        if (typeof options === "string") {
            options = {
                className: options
            };
        }
        this.options = inherit(pluginOptions, $.isPlainObject(options) ? options : {});
        this.loadHTML();
        this.wrapper = $(coreStyle.html);
        if (this.options.clickToHide) {
            this.wrapper.addClass(pluginClassName + "-hidable");
        }
        this.wrapper.data(pluginClassName, this);
        this.arrow = this.wrapper.find("." + pluginClassName + "-arrow");
        this.container = this.wrapper.find("." + pluginClassName + "-container");
        this.container.append(this.userContainer);
        if (elem && elem.length) {
            this.elementType = elem.attr("type");
            this.originalElement = elem;
            this.elem = getAnchorElement(elem);
            this.elem.data(pluginClassName, this);
            this.elem.before(this.wrapper);
        }
        this.container.hide();
        this.run(data);
    }

    Notification.prototype.loadHTML = function () {
        var style;
        style = this.getStyle();
        this.userContainer = $(style.html);
        this.userFields = style.fields;
    };

    Notification.prototype.show = function (show, userCallback) {
        var args, callback, elems, fn, hidden;
        callback = (function (_this) {
            return function () {
                if (!show && !_this.elem) {
                    _this.destroy();
                }
                if (userCallback) {
                    return userCallback();
                }
            };
        })(this);
        hidden = this.container.parent().parents(':hidden').length > 0;
        elems = this.container.add(this.arrow);
        args = [];
        if (hidden && show) {
            fn = "show";
        } else if (hidden && !show) {
            fn = "hide";
        } else if (!hidden && show) {
            fn = this.options.showAnimation;
            args.push(this.options.showDuration);
        } else if (!hidden && !show) {
            fn = this.options.hideAnimation;
            args.push(this.options.hideDuration);
        } else {
            return callback();
        }
        args.push(callback);
        return elems[fn].apply(elems, args);
    };

    Notification.prototype.setGlobalPosition = function () {
        var p = this.getPosition();
        var pMain = p[0];
        var pAlign = p[1];
        var main = positions[pMain];
        var align = positions[pAlign];
        var key = pMain + "|" + pAlign;
        var anchor = globalAnchors[key];
        if (!anchor) {
            anchor = globalAnchors[key] = createElem("div");
            var css = {};
            css[main] = 0;
            if (align === "middle") {
                css.top = '45%';
            } else if (align === "center") {
                css.left = '45%';
            } else {
                css[align] = 0;
            }
            css.width='99%';
            anchor.css(css).addClass(pluginClassName + "-corner");
            $("body").append(anchor);
        }
        return anchor.prepend(this.wrapper);
    };

    Notification.prototype.setElementPosition = function () {
        var arrowColor, arrowCss, arrowSize, color, contH, contW, css, elemH, elemIH, elemIW, elemPos, elemW, gap, j, k, len, len1, mainFull, margin, opp, oppFull, pAlign, pArrow, pMain, pos, posFull, position, ref, wrapPos;
        position = this.getPosition();
        pMain = position[0];
        pAlign = position[1];
        pArrow = position[2];
        elemPos = this.elem.position();
        elemH = this.elem.outerHeight();
        elemW = this.elem.outerWidth();
        elemIH = this.elem.innerHeight();
        elemIW = this.elem.innerWidth();
        wrapPos = this.wrapper.position();
        contH = this.container.height();
        contW = this.container.width();
        mainFull = positions[pMain];
        opp = opposites[pMain];
        oppFull = positions[opp];
        css = {};
        css[oppFull] = pMain === "b" ? elemH : pMain === "r" ? elemW : 0;
        incr(css, "top", elemPos.top - wrapPos.top);
        incr(css, "left", elemPos.left - wrapPos.left);
        ref = ["top", "left"];
        for (j = 0, len = ref.length; j < len; j++) {
            pos = ref[j];
            margin = parseInt(this.elem.css("margin-" + pos), 10);
            if (margin) {
                incr(css, pos, margin);
            }
        }
        gap = Math.max(0, this.options.gap - (this.options.arrowShow ? arrowSize : 0));
        incr(css, oppFull, gap);
        if (!this.options.arrowShow) {
            this.arrow.hide();
        } else {
            arrowSize = this.options.arrowSize;
            arrowCss = $.extend({}, css);
            arrowColor = this.userContainer.css("border-color") || this.userContainer.css("border-top-color") || this.userContainer.css("background-color") || "white";
            for (k = 0, len1 = mainPositions.length; k < len1; k++) {
                pos = mainPositions[k];
                posFull = positions[pos];
                if (pos === opp) {
                    continue;
                }
                color = posFull === mainFull ? arrowColor : "transparent";
                arrowCss["border-" + posFull] = arrowSize + "px solid " + color;
            }
            incr(css, positions[opp], arrowSize);
            if (indexOf.call(mainPositions, pAlign) >= 0) {
                incr(arrowCss, positions[pAlign], arrowSize * 2);
            }
        }
        if (indexOf.call(vAligns, pMain) >= 0) {
            incr(css, "left", realign(pAlign, contW, elemW));
            if (arrowCss) {
                incr(arrowCss, "left", realign(pAlign, arrowSize, elemIW));
            }
        } else if (indexOf.call(hAligns, pMain) >= 0) {
            incr(css, "top", realign(pAlign, contH, elemH));
            if (arrowCss) {
                incr(arrowCss, "top", realign(pAlign, arrowSize, elemIH));
            }
        }
        if (this.container.is(":visible")) {
            css.display = "block";
        }
        this.container.removeAttr("style").css(css);
        if (arrowCss) {
            return this.arrow.removeAttr("style").css(arrowCss);
        }
    };

    Notification.prototype.getPosition = function () {
        var pos, ref, ref1, ref2, ref3, ref4, ref5, text;
        text = this.options.position || (this.elem ? this.options.elementPosition : this.options.globalPosition);
        pos = parsePosition(text);
        if (pos.length === 0) {
            pos[0] = "b";
        }
        if (ref = pos[0], indexOf.call(mainPositions, ref) < 0) {
            throw "Must be one of [" + mainPositions + "]";
        }
        if (pos.length === 1 || ((ref1 = pos[0], indexOf.call(vAligns, ref1) >= 0) && (ref2 = pos[1], indexOf.call(hAligns, ref2) < 0)) || ((ref3 = pos[0], indexOf.call(hAligns, ref3) >= 0) && (ref4 = pos[1], indexOf.call(vAligns, ref4) < 0))) {
            pos[1] = (ref5 = pos[0], indexOf.call(hAligns, ref5) >= 0) ? "m" : "l";
        }
        if (pos.length === 2) {
            pos[2] = pos[1];
        }
        return pos;
    };

    Notification.prototype.getStyle = function (name) {
        var style;
        if (!name) {
            name = this.options.style;
        }
        if (!name) {
            name = "default";
        }
        style = styles[name];
        if (!style) {
            throw "Missing style: " + name;
        }
        return style;
    };

    Notification.prototype.updateClasses = function () {
        var classes, style;
        classes = ["base"];
        if ($.isArray(this.options.className)) {
            classes = classes.concat(this.options.className);
        } else if (this.options.className) {
            classes.push(this.options.className);
        }
        style = this.getStyle();
        classes = $.map(classes, function (n) {
            return pluginClassName + "-" + style.name + "-" + n;
        }).join(" ");
        return this.userContainer.attr("class", classes);
    };

    Notification.prototype.run = function (data, options) {
        var d, datas, name, type, value;
        if ($.isPlainObject(options)) {
            $.extend(this.options, options);
        } else if ($.type(options) === "string") {
            this.options.className = options;
        }
        if (this.container && !data) {
            this.show(false);
            return;
        } else if (!this.container && !data) {
            return;
        }
        datas = {};
        if ($.isPlainObject(data)) {
            datas = data;
        } else {
            datas[blankFieldName] = data;
        }
        for (name in datas) {
            d = datas[name];
            type = this.userFields[name];
            if (!type) {
                continue;
            }
            if (type === "text") {
                d = encode(d);
                if (this.options.breakNewLines) {
                    d = d.replace(/\n/g, '<br/>');
                }
            }
            value = name === blankFieldName ? '' : '=' + name;
            find(this.userContainer, "[data-notify-" + type + value + "]").html(d);
        }
        this.updateClasses();
        if (this.elem) {
            this.setElementPosition();
        } else {
            this.setGlobalPosition();
        }
        this.show(true);
        if (this.options.autoHide) {
            clearTimeout(this.autohideTimer);
            this.autohideTimer = setTimeout(this.show.bind(this, false), this.options.autoHideDelay);
        }
    };

    Notification.prototype.destroy = function () {
        this.wrapper.data(pluginClassName, null);
        this.wrapper.remove();
    };

    $[pluginName] = function (elem, data, options) {
        if ((elem && elem.nodeName) || elem.jquery) {
            $(elem)[pluginName](data, options);
        } else {
            options = data;
            data = elem;
            new Notification(null, data, options);
        }
        return elem;
    };

    $.fn[pluginName] = function (data, options) {
        $(this).each(function () {
            var prev = getAnchorElement($(this)).data(pluginClassName);
            if (prev) {
                prev.destroy();
            }
            var curr = new Notification($(this), data, options);
        });
        return this;
    };

    $.extend($[pluginName], {
        defaults: defaults,
        addStyle: addStyle,
        pluginOptions: pluginOptions,
        getStyle: getStyle,
        insertCSS: insertCSS
    });

    //always include the default bootstrap style
    addStyle("bootstrap", {
        html: "<div>\n<span data-notify-text></span>\n</div>",
        classes: {
            base: {
                "font-weight": "bold",
                "padding": "8px 15px 8px 14px",
                "text-shadow": "0 1px 0 rgba(255, 255, 255, 0.5)",
                "background-color": "#fcf8e3",
                "border": "1px solid #fbeed5",
                "border-radius": "4px",
                "white-space": "nowrap",
                "padding-left": "50px",
                "background-repeat": "no-repeat",
                "background-position": "3px 7px"
            },
            error: {
                "font-size": "20pt",
                "color": "#B94A48",
                "background-color": "#F2DEDE",
                "border-color": "#EED3D7",
                "background-image": "url(data:image/png;base64,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)"
            },
            success: {
                "font-size":"20pt",
                "font-weight": "bold",
                "color": "#468847",
                "background-color": "#DFF0D8",
                "border-color": "#D6E9C6",
                "background-image": "url(data:image/png;base64,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)"
            },
            info: {
                "font-size": "20pt",
                "color": "#3A87AD",
                "background-color": "#D9EDF7",
                "border-color": "#BCE8F1",
                "background-image": "url(data:image/png;base64,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)"
            },
            warn: {
                "font-size": "20pt",
                "color": "#C09853",
                "background-color": "#FCF8E3",
                "border-color": "#FBEED5",
                "background-image": "url(data:image/png;base64,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)"
            }
        }
    });

    $(function () {
        insertCSS(coreStyle.css).attr("id", "core-notify");
        $(document).on("click", "." + pluginClassName + "-hidable", function (e) {
            $(this).trigger("notify-hide");
        });
        $(document).on("notify-hide", "." + pluginClassName + "-wrapper", function (e) {
            var elem = $(this).data(pluginClassName);
            if (elem) {
                elem.show(false);
            }
        });
    });

}));
