$(document).ready(function () {
	loadTracker($("#tracker"));
	$("body").on("keypress", function(evt) {
		if ($(".selected").length == 0) { return; }
		var label = $(".selected.note_label").first();
		label.text(evt.key);
	});
});

var loadTracker = function (tracker) {
	for (var i = 0; i < 8; i++) {
		appendTrack(tracker);
	}
};

var appendTrack = function(tracker) {
	var column = $(document.createElement("div")).addClass("track_line").appendTo(tracker);
	$(document.createElement("div")).addClass("label").on("dblclick", function(evt) {
        var menu = $(document.createElement("select")).css({ top: evt.clientY, left: evt.clientX, position: "absolute" }).
            on("change", function (evt) {
			$("select option:selected").each(function() {
				console.log(this.text());
				menu.remove();
			})
		}).appendTo($("body"));
	}).appendTo(column);

	for (var i=0; i < 32; i++) {
		var node = $(document.createElement("div")).addClass("node").on("click", function(evt) {
			$(".selected").removeClass("selected");
			$(evt.target).addClass("selected");
		}).appendTo(column);
		$(document.createElement("div")).addClass("trigger").appendTo(node);
		$(document.createElement("div")).addClass("note_label").appendTo(node);
	}
	$(document.createElement("div")).addClass("clear").appendTo(column);
}

var getSongData = function() {
	var lines = [];
	var tracks = $(".track_line");
	for (var lineIndex=0; lineIndex<32; lineIndex++) {
		var line = "";
		for (var trackIndex=0; trackIndex<8; trackIndex++) {
			$(tracks[trackIndex]).find(".node").each(function(idx, node) {
				console.log()
				if (node.text() != "") { line += `$(node.text())`; }
			})
		}
		lines.push(line);
	}
	return lines;
}