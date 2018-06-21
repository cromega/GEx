var notes = {
    "q": "c",
    "a": "c#",
    "w": "d",
    "s": "d#",
    "e": "e",
    "r": "f",
    "f": "f#",
    "t": "g",
    "g": "g#",
    "y": "a",
    "h": "a#",
    "u": "b",
};

var octave = 4;

var setOctave = function (oct) {
    octave = oct;
    $("#octaveLabel").text(octave);
}

var step = function (x, y) {
    var selected = $(".selected").first();
    var maxx = $(".track_line").length;
    var maxy = $(".track_line").first().find(".node").length;

    var newx = selected.data("x") + x;
    var newy = selected.data("y") + y;
    if (newx >= maxx) { newx = 0; }
    if (newx < 0) { newx = maxx - 1}
    if (newy >= maxy) { newy = 0; }
    if (newy < 0) { newy = maxy - 1 }

    $(".selected").removeClass("selected");
    $(".node[data-x='" + newx + "'][data-y='" + newy + "']").addClass("selected");
}

$(document).ready(function () {
	loadTracker($("#tracker"));

	$("body").on("keydown", function(evt) {
        if (evt.keyCode >= 49 && evt.keyCode <= 56) {
            setOctave(evt.keyCode - 48);
            return;
        }

        //window.external.DebugLog(evt.key.toString());
        var label = $(".selected .note_label").first();
        if (label.length == 0) { return; }
        switch (evt.key) {
            case ";":
                step(-1, 0);
                break;
            case "'":
                step(0, 1);
                break;
            case "\\":
                step(1, 0);
                break;
            case "[":
                step(0, -1);
                break;
            case "z":
                label.text("");
                break;
            case "q":
            case "a":
            case "w":
            case "s":
            case "e":
            case "r":
            case "f":
            case "t":
            case "g":
            case "y":
            case "h":
            case "u":
                label.text(notes[evt.key] + octave);
                break;
        }
	});
});

var loadTracker = function (tracker) {
	for (var i = 0; i < 8; i++) {
		appendTrack(tracker, i);
	}
};

var appendTrack = function(tracker, trackId) {
	var column = $(document.createElement("div")).addClass("track_line").appendTo(tracker);
	//$(document.createElement("div")).addClass("label").on("dblclick", function(evt) {
    //       var menu = $(document.createElement("select")).css({ top: evt.clientY, left: evt.clientX, position: "absolute" }).
    //           on("change", function (evt) {
	//		$("select option:selected").each(function() {
	//			console.log(this.text());
	//			menu.remove();
	//		})
	//	}).appendTo($("body"));
	//}).appendTo(column);

	for (var i=0; i < 32; i++) {
		var node = $(document.createElement("div")).addClass("node").on("click", function(evt) {
			$(".selected").removeClass("selected");
            console.log(evt.target);
			$(evt.target).addClass("selected");
		}).attr("data-x", trackId).attr("data-y", i).appendTo(column);
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
            var node = $(tracks[trackIndex]).find(".node")[lineIndex];
            var text = $(node).text();

            if (text != "") {
                var note = text.slice(0, -1);
                var octave = parseInt(text.slice(-1));
                var noteindexes = Object.keys(notes).map(function (e) {
                    return notes[e];
                });
                var noteidx = noteindexes.indexOf(note) + 1 + octave * 12;
                line += "0;3;" + noteidx + " ";
            } else {
                line += "- ";
            }
		}
        lines.push(line.trim());
	}
    return lines.join("|");
}