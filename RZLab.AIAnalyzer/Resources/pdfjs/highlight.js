// highlight.js - PDF.js Overlay Highlighter for WebView2
// Author: ChatGPT (Optimized for PDF.js legacy 2.x)

(function (window) {

    const H_CLASS = 'pdfjs-overlay-highlight';

    // Inject highlight style
    (function ensureStyle() {
        if (document.getElementById('pdfjs-highlight-style')) return;
        const style = document.createElement('style');
        style.id = 'pdfjs-highlight-style';
        style.textContent = `
            .${H_CLASS} {
                position: absolute;
                background: rgba(255, 240, 0, 0.45);
                pointer-events: none;
                border-radius: 3px;
                z-index: 30;
            }
        `;
        document.head.appendChild(style);
    })();


    // Remove all highlights
    window.clearAllHighlights = function () {
        document.querySelectorAll('.' + H_CLASS).forEach(e => e.remove());
    };


    function addOverlayToPage(pageDiv, rect, color) {
        const overlay = document.createElement('div');
        overlay.className = H_CLASS;
        overlay.style.left = rect.left + "px";
        overlay.style.top = rect.top + "px";
        overlay.style.width = rect.width + "px";
        overlay.style.height = rect.height + "px";
        overlay.style.background = color || "rgba(255,240,0,0.45)";
        overlay.style.position = "absolute";
        overlay.style.pointerEvents = "none";
        overlay.style.borderRadius = "4px";
        pageDiv.appendChild(overlay);
    }

    // Convert absolute browser rect to page-relative rect
    function toRelativeRect(spanRect, pageRect) {
        return {
            left: spanRect.left - pageRect.left,
            top: spanRect.top - pageRect.top,
            width: spanRect.width,
            height: spanRect.height
        };
    }

    // Wait until textLayer for a page exists
    function waitForTextLayer(pageDiv, timeout = 3000) {
        return new Promise((resolve, reject) => {
            const layer = pageDiv.querySelector('.textLayer');
            if (layer && layer.children.length > 0) return resolve(layer);

            const obs = new MutationObserver(() => {
                const layer = pageDiv.querySelector('.textLayer');
                if (layer && layer.children.length > 0) {
                    obs.disconnect();
                    resolve(layer);
                }
            });
            obs.observe(pageDiv, { childList: true, subtree: true });

            setTimeout(() => {
                obs.disconnect();
                const layer2 = pageDiv.querySelector('.textLayer');
                if (layer2 && layer2.children.length > 0) resolve(layer2);
                else reject('textLayer timeout');
            }, timeout);
        });
    }


    // 🔥 Main API: highlight text in ALL pages
    window.highlightAllPages = async function (keywords, opts = {}) {
        if (!keywords) return;

        // Normalize list
        if (typeof keywords === "string") {
            try { keywords = JSON.parse(keywords); }
            catch { keywords = [keywords]; }
        }

        keywords = keywords.map(k => {
            if (typeof k === "string") return { text: k, color: "rgba(255,240,0,0.45)" };
            return {
                text: (k.text || "").toLowerCase(),
                color: k.color || "rgba(255,240,0,0.45)"
            };
        });

        if (!opts.keep) window.clearAllHighlights();

        const viewer = document.getElementById("viewer");
        const pages = Array.from(viewer.querySelectorAll(".page"));

        for (const pageDiv of pages) {
            let textLayer;

            try {
                textLayer = await waitForTextLayer(pageDiv);
            }
            catch { continue; }

            const spans = Array.from(textLayer.querySelectorAll("span"));
            if (!spans.length) continue;

            const pageRect = pageDiv.getBoundingClientRect();

            for (const span of spans) {
                const raw = (span.textContent || "");
                const lower = raw.toLowerCase();

                for (const k of keywords) {
                    if (!k.text || lower.indexOf(k.text) === -1) continue;

                    const sRect = span.getBoundingClientRect();
                    const rel = toRelativeRect(sRect, pageRect);

                    if (rel.height < 10) rel.height = 12;

                    addOverlayToPage(pageDiv, rel, k.color);
                    break;
                }
            }
        }
    };


    // Convenience API → C# passes an array
    window.highlightMultiple = function (json) {
        try {
            console.log(json);
            const arr = typeof json === "string" ? JSON.parse(json) : json;
            window.highlightAllPages(arr);
        } catch (e) {
            console.error("highlightMultiple error:", e);
        }
    };


    // Scroll to first highlighted element
    window.scrollToFirstHighlight = function () {
        const h = document.querySelector('.' + H_CLASS);
        if (h) {
            const page = h.closest('.page');
            if (page) page.scrollIntoView({ behavior: "smooth", block: "center" });
        }
    };

})(window);