import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { SpinnerService } from './spinner.service';


@Component({
  selector: 'pm-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit,OnDestroy  {
  //post link http://codetunnel.io/how-to-do-loading-spinners-the-angular-2/
  @Input() name: string;
  @Input() group: string;
  @Input() loadingImage: string;
  @Input() isLoadingOverlay:boolean;
  // @Input() show = false;

  private isShowing = false;
  
    @Input()
    get show(): boolean {
      return this.isShowing;
    }
  
    @Output() showChange = new EventEmitter();
  
    set show(val: boolean) {
      this.isShowing = val;
      this.showChange.emit(this.isShowing);
    }

  constructor(private spinnerService: SpinnerService) { }

  

  ngOnInit() {
    if (!this.name) throw new Error("Spinner must have a 'name' attribute.");
    this.spinnerService._register(this);
  }

  ngOnDestroy(): void {
    this.spinnerService._unregister(this);
  }

  
}
