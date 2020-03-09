/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PartgridComponent } from './partgrid.component';

describe('PartgridComponent', () => {
  let component: PartgridComponent;
  let fixture: ComponentFixture<PartgridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PartgridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PartgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
