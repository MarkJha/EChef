import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'summeryText'
})
export class SummeryTextPipe implements PipeTransform {

  transform(value: string, limit?: number): any {
    if (!value)
      return null;

    let actualLimit = (limit) ? limit : 30;
    return value.substring(0, actualLimit) + '...';
  }

}
