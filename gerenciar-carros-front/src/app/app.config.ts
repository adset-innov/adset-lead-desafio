import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';
import { IConfig, provideEnvironmentNgxMask } from 'ngx-mask';
const maskConfig: Partial<IConfig> = {
  validation: false,
};
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimationsAsync(), provideHttpClient(), provideEnvironmentNgxMask(maskConfig)]
};
