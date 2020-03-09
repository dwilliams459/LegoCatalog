/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PartlistComponent } from './partlist.component';

describe('PartlistComponent', () => {
  let component: PartlistComponent;
  let fixture: ComponentFixture<PartlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PartlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
