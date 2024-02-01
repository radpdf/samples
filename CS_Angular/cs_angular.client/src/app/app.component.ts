import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

interface RadPdfInstance {
  clientID: string;
  frameUrl: string;
  safeFrameUrl: SafeResourceUrl;
  viewState: string;
  viewStateID: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public radpdf?: RadPdfInstance;

  constructor(private http: HttpClient, public sanitizer: DomSanitizer) {}

  ngOnInit() {
    this.getRadPdfInstance();
  }

  getRadPdfInstance() {
    // default is a placeholder for any data / ID you wish to pass to the server
    this.http.get<RadPdfInstance>('/radpdf/default').subscribe(
      (result) => {
        // URL must be marked as safe; we know it will be safe by prefixing with absolute directory
        result.safeFrameUrl = this.sanitizer.bypassSecurityTrustResourceUrl('/radpdf/' + result.frameUrl);
        this.radpdf = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'cs_angular.client';
}
