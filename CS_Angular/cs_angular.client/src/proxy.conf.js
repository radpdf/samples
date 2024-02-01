const PROXY_CONFIG = [
  {
    context: [
      "/radpdf",
      "RadPdf.axd",
      "/weatherforecast",
    ],
    target: "https://localhost:7212",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
