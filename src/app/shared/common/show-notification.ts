import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { AppError } from './app-error';
import { BadInput } from './bad-input';
import { NotFoundError } from './not-found-error';
import { ServerDown } from './server-down';

@Injectable()
export class ShowNotificationService {
    constructor(private _toastr: ToastrService) {

    }

    public showInfo(message: string) {
        this._toastr.info(message);
    }

    public showSuccess(message: string) {
        this._toastr.success(message);
    }

    public showWarning(message: string) {
        this._toastr.warning(message);
    }

    public showError(error: AppError) {
        if (error instanceof ServerDown) {
            this._toastr.error('server is down');
        }
        else if (error instanceof BadInput) {
            this._toastr.error('input is not in correct format');
        }
        else if (error instanceof NotFoundError) {
            this._toastr.error('request is not found');
        } else {
            this._toastr.error(error.originalError);
        }
    }

}