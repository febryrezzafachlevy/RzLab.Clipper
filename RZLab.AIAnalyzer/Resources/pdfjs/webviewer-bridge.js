// webviewer-bridge.js
(function (window) {
    // notify host (C#) via postMessage
    function sendHostMessage(obj) {
        try {
            const payload = JSON.stringify(obj);
            if (window.chrome && chrome.webview && chrome.webview.postMessage) {
                chrome.webview.postMessage(payload);
            } else if (window.external && window.external.notify) {
                window.external.notify(payload);
            } else {
                // no-op
            }
        } catch (e) { console.error(e); }
    }

    window.bridge = {
        // C# -> JS: load PDF url (virtual host)
        loadPdf: function (url) {
            if (!url) return;
            if (window.PDFViewerApplication) {
                PDFViewerApplication.open(url);
            } else {
                window.DEFAULT_PDF_URL = url;
            }
        },
        // C# -> JS highlight list: pass JSON array or stringified JSON
        highlight: function (payload) {
            try {
                if (typeof payload === 'string') payload = JSON.parse(payload);
            } catch (e) { }
            window.highlightKeywords(payload);
        },
        // C# -> JS: jump to page
        jumpToPage: function (page) {
            try {
                if (window.PDFViewerApplication) PDFViewerApplication.page = parseInt(page, 10);
            } catch (e) { console.error(e); }
        },
        // JS -> host
        sendMessage: function (type, data) {
            sendHostMessage({ type: type, data: data });
        }
    };

    // allow C# to wait for ready
    window.addEventListener('PDFViewerLoaded', function () {
        try { window.bridge.sendMessage('viewer_ready', { pages: PDFViewerApplication.pdfDocument.numPages }); }
        catch (e) { }
    });
    PDFViewerApplicationOptions.set('disableThumbnail', true);
    PDFViewerApplicationOptions.set('disableHistory', true);
})(window);
