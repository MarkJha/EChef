import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'pm-search-panel',
  templateUrl: './search-panel.component.html',
  styleUrls: ['./search-panel.component.css']
})
export class SearchPanelComponent implements OnInit {
  @Input() name: string;
  @Input() placeHolder: string;
  @Output() searchTextClicked: EventEmitter<string> =
  new EventEmitter<string>();

  @Output() searchOnKeyup: EventEmitter<string> =
  new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
    if (!this.name) throw new Error("Spinner must have a 'name' attribute.");
  }

  onClick(text): void {
    console.log(text);
    this.searchTextClicked.emit(text);
  }

  onKeyup(keyupText):void{
    console.log(keyupText);
    this.searchOnKeyup.emit(keyupText);
  }
}
