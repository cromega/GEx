$(document).ready(function () {
	loadTracker($("#tracker"));
});

var loadTracker = function (tracker) {
	for (var i = 0; i < 8; i++) {
		appendTrack(tracker);
	}
};

var appendTrack = function (tracker) {
	$(tracker).append("<div class=\"track_line\"></div>");
	var line = $(".track_line").last();
	for (var i = 0; i < 32; i++) {
		line.append("<div class=\"node\"></div>");
	}
	line.append("<div class=\"clear\"></div>");
}