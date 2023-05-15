import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TransportCard } from '../models/transport-card';
import { BaseResponse } from '../models/base-response';

@Injectable({
    providedIn: 'root',
})
export class TransportCardService {
    private _apiUrl: string = environment.apiUrl;
    constructor(private _httpClient: HttpClient) {}

    public post(params: TransportCard): Observable<BaseResponse<any>> {
    const url = `${this._apiUrl}/api/transport-card/add`;
    return this._httpClient.post<BaseResponse<any>>(url, params);
    }

    public enterStation(id: string | number, params: any = null): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/enter-station`;
        return this._httpClient.put<BaseResponse<any>>(url, params);
    }

    public exitStation(id: string | number, params: any = null, isCount: any = null): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/exit-station` + `${isCount == true ? `?isCount=true` : ``}`;
        return this._httpClient.put<BaseResponse<any>>(url, params);
    }
    public put(id: string | number, params: any = null): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/update`;
        return this._httpClient.put<BaseResponse<any>>(url, params);
    }

    public addLoadAmount(id: string | number, params: any = null): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/add-load-amount`;
        return this._httpClient.put<BaseResponse<any>>(url, params);
    }

    public limitLoadAmount(id: string | number, params: any = null): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/limit-load-amount`;
        return this._httpClient.put<BaseResponse<any>>(url, params);
    }

    public delete(id: string | number): Observable<BaseResponse<any>> {
        const url = `${this._apiUrl}/api/transport-card/${id}/delete`;
        return this._httpClient.delete<BaseResponse<any>>(url);
    }
    public get(id: string | number = ''): Observable<BaseResponse<TransportCard[]>> {
        const url = `${this._apiUrl}/api/transport-card/get`;
        return this._httpClient.get<BaseResponse<TransportCard[]>>(url, { params: { id: id } });
    }    
    public getTransportCard(params: any = null): Observable<BaseResponse<TransportCard[]>> {
        const url = `${this._apiUrl}/api/transport-card/discounted/get`;
        return this._httpClient.get<BaseResponse<TransportCard[]>>(url, { params: params });
    }
    public getTransportCardEntryCount(params: any = null): Observable<BaseResponse<TransportCard[]>> {
        const url = `${this._apiUrl}/api/transport-card/entry-count/get`;
        return this._httpClient.get<BaseResponse<TransportCard[]>>(url, { params: params });
    }
}
