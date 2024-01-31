import { useEffect, useState } from 'react';

function RadPdf() {

    const [radpdfinstance, setRadPdf] = useState();

    useEffect(() => {
        renderRadPdf();
    }, []);

    return radpdfinstance ? (
            <div dangerouslySetInnerHTML={{ __html: radpdfinstance }} />
        ) : (
            <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        );

    async function renderRadPdf(id) {
        await fetch('radpdf/' + (id || 'default'))
            .then(function (response) {
                if (response.ok) {
                    return response.text();
                }
            })
            .then(function (html) {

                setRadPdf(html);
            });
    }
}

export default RadPdf;