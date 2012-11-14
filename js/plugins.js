// Avoid `console` errors in browsers that lack a console.
(function() {
    var method;
    var noop = function noop() {};
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    var console = (window.console = window.console || {});

    while (length--) {
        method = methods[length];

        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }
}());

function getParameterByName(name)
{
	name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
	var regexS = "[\\?&]" + name + "=([^&#]*)";
	var regex = new RegExp(regexS);
	var results = regex.exec(window.location.search);
	if (results == null) {
		return "";
	} else {
		return decodeURIComponent(results[1].replace(/\+/g, " "));
	}
}

// Place any jQuery/helper plugins in here.
$(".menu").mouseenter(function () {
	var $this = $(this);
	var menuSelector = "." + $this.attr("data-menu");
	var $menu = $(menuSelector);
	
	$this.children("a:first-child").addClass("menu-open");
	$menu.removeClass("visuallyhidden");
});

$(".menu").mouseleave(function () {
	var $this = $(this);
	var menuSelector = "." + $this.attr("data-menu");
	var $menu = $(menuSelector);
	
	$this.children("a:first-child").removeClass("menu-open");
	$menu.addClass("visuallyhidden");
});

$(function () {
	var path = window.location.pathname;
	var page = path.substring(path.lastIndexOf('/') + 1, path.lastIndexOf('.'));
	var document = page.match(/.*?(?:(?=_\d+_\d+$)|$)/)[0];
	var versionMatches = page.match(/_\d+_\d+$/);
	var versionParts;
	
	if (versionMatches !== null) {
		versionParts = versionMatches[0].substring(1).split("_");
	}
	
	$(".document .version a").each(function () {
		var $this = $(this);
		var version = $this.attr("data-version");
		
		if ((version === undefined && versionParts === undefined) ||
			(versionParts !== undefined && versionParts.join(".") === version)) {
			$this.addClass("current-document");
		}
		if (version !== undefined) {
			$this.attr("href", document + "_" + version.split(".").join("_") + ".html");
		} else {
			$this.attr("href", document + ".html");
		}
	});
});