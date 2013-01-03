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
                            items[i].getAsString(textHandler);
                            break;
                        }
                        if (items[i].kind == "file" && items[i].type.indexOf("image") == 0) {
                            var reader = new FileReader();
                            reader.onload = function (event) {
                                imageHandler(event.target.result); //event.target.results contains the base64 code to create the image.
                            };
                            reader.readAsDataURL(items[i].getAsFile());
                            break;
                        }
                    }
                }
            } else {
                setTimeout(checkInput, 1);
            }

            isPasteHandlerEnabled = false;
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
    }

    function textHandler(content) {
        /// <summary>Handles text adding to the container.</summary>
        content = content.replace(/<br>/gi, "\r\n");
        content = content.replace(/(<([^>]+)>)/ig, "")
        $("div.main form #Content").empty()
                                   .prop("contenteditable", "false")
                                   .append("<textarea>" + content + "</textarea>");

        $("div.main form #Content textarea").focus();
    }

    function imageHandler(content) {
        /// <summary>Handles image adding to the container.</summary>
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
            if (content == "") {
                content = $("div.main form #Content").text();
            }
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
                $("div.main h1").html("Saved to: <a href='" + window.location + id + "'>" + window.location + id + "</a>");
            },
            "json"
        )

        var main = $("div.main");

        main.slideUp(function () {
            $("div.main form").remove();
            if (undefined == id) {
                main.prepend("<h1>Saving...</h1>");
            } else {
                main.prepend("<h1>Saved to: <a href='" + window.location + id + "'>" + window.location + id + "</a></h1>");
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
