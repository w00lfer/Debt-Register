/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LentComponent } from './lent.component';

describe('LentComponent', () => {
  let component: LentComponent;
  let fixture: ComponentFixture<LentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
