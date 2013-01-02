$(document).ready(function () {
    var isPasteHandlerEnabled = true;

    function pasteHandler(jQueryEvent) {
        /// <summary>Handles Ctrl+V event.</summary>
        if (isPasteHandlerEnabled) {
            var event = jQueryEvent.originalEvent;
            if (event.clipboardData) {
                var items = event.clipboardData.items;
                if (items) {
                    for (var i = 0; i < items.length; ++i) {
                        if (items[i].kind == "string" && items[i].type == "text/plain") {
                            var text = items[i].getAsString(); // handleTextLoad
                            alert(text);
                            break;
                        }
                        if (items[i].kind == "file" && items[i].type.indexOf("image") == 0) {
                            var blob = items[i].getAsFile();
                            loadBlob(blob);
                            break;
                        }
                    }
                }
                // alert("chrome");
            } else {
                setTimeout(checkInput, 1);
            }
        }
    }

    function checkInput() {
        var content;
        if ($("div.main form #Content img").size() == 0) {
            // Only text is inserted
            textHandler($("div.main form #Content").html());
        } else {
            // Image is inserted
            imageHandler($("div.main form #Content img").prop("src"));
        }

        isPasteHandlerEnabled = false;
    }

    function textHandler(content) {
        content = content.replace(/<br>/gi, "\r\n");
        content = content.replace(/(<([^>]+)>)/ig, "")
        $("div.main form #Content").empty()
                                   .prop("contenteditable", "false")
                                   .append("<textarea>" + content + "</textarea>");

        $("div.main form #Content textarea").focus();
    }

    function imageHandler(content) {
        $("div.main form #Content").empty()
                                   .prop("contenteditable", "false")
                                   .append("<img src='" + content + "' />");

        $("div.main form #Content img").focus();
    }

    function submitHandler(event) {
        /// <summary>Handles submit event.</summary>
        var content;
        var isText = ($("div.main form #Content img").size() == 0) ? true : false;


        if (isText) {
            // Only text is inserted
            content = $("div.main form #Content textarea").text();
        } else {
            // Image is inserted
            content = $("div.main form #Content").html();
        }

        if (content == "") {
            return false;
        }

        event.preventDefault();

        var id;

        $.post(
            "/Create",
            { "Content": content },
            function (pastie) {
                id = pastie.Id;
                $("div.main h1").text("#" + pastie.Id + "");
            },
            "json"
        )

        var main = $("div.main");

        main.slideUp(function () {
            $("div.main form").remove();
            if (undefined == id) {
                main.prepend("<h1>#</h1>");
            } else {
                main.prepend("<h1>#" + id + "</h1>");
            }

            if (isText) {
                main.append("<pre><code class='prettyprint linenums'>" + $('<div/>').text(content).html() + "</code></pre>");
            } else {
                main.append("<div id='Content'>" + content + "</div>");
            }
            main.slideDown();

            $.getScript(
                "/content/js/vendor/prettify.js",
                function () {
                    prettyPrint();
                });
        });
    }

    $(window).on('paste', pasteHandler);
    $("div.main form input:submit").click(submitHandler);
});	
