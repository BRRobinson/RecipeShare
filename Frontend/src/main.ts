import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { bootstrapApplication } from '@angular/platform-browser';
import { API_URL, appConfig } from './app/app.config';
import { App } from './app/app';
import { environment } from './environments/environment.development';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

bootstrapApplication(App, {
    ...appConfig,
    providers: [
      ...(appConfig.providers || []),
      { provide: API_URL, useValue: environment.apiUrl },
      provideRouter(routes)
    ]
  }).catch((err) => console.error(err));