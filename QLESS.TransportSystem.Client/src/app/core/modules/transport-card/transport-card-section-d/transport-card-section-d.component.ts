import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { BaseResponse } from 'projects/models/base-response';
import { TransportCard } from 'projects/models/transport-card';
import { TransportCardService } from 'projects/services/transport-card.service';
import { map, Observable, Subscription, timeout } from 'rxjs';

@Component({
  selector: 'app-transport-card-section-d',
  templateUrl: './transport-card-section-d.component.html',
  styleUrls: ['./transport-card-section-d.component.scss']
})
export class TransportCardSectionDComponent implements OnInit {

  @Output() public page = new EventEmitter<PageEvent>();
  private _subscription = new Subscription();
  
  public transportCardDataLists!: Observable<BaseResponse<any[]>>;
  public transportCardDetailsDataLists!: Observable<BaseResponse<any[]>>;
  public dataTableRows!: number | undefined;
  public pageIndex!:number;
  public pageSize!:number;
  public length!:number;
  public pageOption: any = { pageNumber: 1, pageSize: 10 };
  public transportCardForm: FormGroup;
  public transportCardDetails: any;
  public minCustomerMoney = 0;

  constructor(
    private _transportCardService : TransportCardService,
    private _toastrService: ToastrService,
    private _modalService: NgbModal,
    private _formBuilder: FormBuilder) { 
      this.transportCardForm = this._formBuilder.group({ 
        TransportCardId: [null], 
        CurrentLoadAmount: [null], 
        AddLoadAmount: [null, Validators.compose([Validators.required, Validators.min(100), Validators.max(1000)])],
        CustomerMoney: [null, Validators.compose([Validators.required, Validators.min(100), Validators.max(1000)])],
      });
    }

    
  public get CustomerMoney() {
    return this.transportCardForm.get('CustomerMoney');
  }

  ngOnInit(): void {
      this.getTransportCard();
  }
  
  public open(element:any, content: any) {
    this.transportCardDetails = element;
    this._modalService.open(content);
    this.transportCardForm.get('TransportCardId')?.setValue(element.id);
    this.transportCardForm.get('CurrentLoadAmount')?.setValue(element.loadAmount);
  }

  public addLoadAmount(){
    let params = {
      id: this.transportCardDetails.id,
      addLoadAmount: this.transportCardForm.get('AddLoadAmount')?.value,
      loadAmount: this.transportCardForm.get('CurrentLoadAmount')?.value,
      customerMoney: this.transportCardForm.get('CustomerMoney')?.value
    };

    if (params.addLoadAmount > params.customerMoney){
      this.CustomerMoney?.setValidators([Validators.required, Validators.min(params.addLoadAmount), Validators.max(1000)])
      this.CustomerMoney?.updateValueAndValidity();
      this.minCustomerMoney = params.addLoadAmount;
    } else {
      this.CustomerMoney?.setValidators([Validators.required, Validators.min(100), Validators.max(1000)])
      this.CustomerMoney?.updateValueAndValidity();
    }

    if(this.transportCardForm.valid){
      this._subscription.add(this._transportCardService.limitLoadAmount(Number(params.id), params).subscribe((result) =>{
        if(result.isSuccess){
          this._toastrService.warning('Total Change Amount is: ₱' + result.data, '', { timeOut: 100000 })
          this._toastrService.success('Load Amount is Added');
          this._modalService.dismissAll();
          this.getTransportCard();
        }
      }))
    } else {
      this._toastrService.error('Entered Load Amount is Invalid!');
    }
  }


  public getTransportCard(event?:PageEvent){
    this.pageOption = {
        pageNumber: event?.pageIndex ? event.pageIndex + 1 : 1,
        pageSize: event?.pageSize ? event.pageSize : 10
    };

    let params = { ...this.pageOption};

    this.transportCardDataLists = this._transportCardService.getTransportCard(params).pipe(
      map((result) => {
        this.dataTableRows = result.rows;
        this.pageIndex = event?.pageIndex!;
        this.pageSize = event?.pageSize!;
          return result;
      }));
  }
  public postTransportCard() {
    let transportCard: TransportCard = {
      isDiscounted: false
    }

    this._subscription.add(
      this._transportCardService.post(transportCard).subscribe((result) => {
        if(result.isSuccess){
          this.transportCardDetailsDataLists = this._transportCardService.get(Number(result.data)).pipe(
            map((result) => {
                return result;
            }));

            this.getTransportCard();

            this._toastrService.success('Sucessfully Created');
        } else {
          this._toastrService.error(result.message);
        }
      })
    )
  }
}
