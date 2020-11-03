import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { partition } from 'rxjs';
declare var google: any;

@Component({
  selector: 'app-counter-component',
  templateUrl: './doanhthu.component.html'
})
export class DoanhThuComponent {
  month: any;
  year: any;
  doanhThu: any = {
    data: []
  }

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { }


  ngOnInit() {

  }

  getDoanhThu() {
    let x = {
      ten: "string",
      dateFrom: "2020-07-01T05:38:55.436Z",
      dateTo: "2020-07-01T05:38:55.436Z",
      isQuantity: true,
      page: 0,
      size: 0,
      month: parseInt(this.month),
      year: parseInt(this.year),
      orderId: 0,
      customerId: "string",
      employeeId: 0,
      orderDate: "2020-07-01T05:38:55.436Z",
      requiredDate: "2020-07-01T05:38:55.436Z",
      shippedDate: "2020-07-01T05:38:55.436Z",
      shipVia: 0,
      freight: 0,
      shipName: "string",
      shipAddress: "string",
      shipCity: "string",
      shipRegion: "string",
      shipPostalCode: "string",
      shipCountry: "string"
    }

    this.http.post('https://localhost:44377' + '/api/Orders/SP-Danh-Sach-Doanh-Thu-Theo-QG-Linq', x).subscribe(result => {
      this.doanhThu = result;
      console.log(this.doanhThu);
      this.drawPieChart(this.doanhThu.data);
    }, error => console.error(error));
  }

  drawPieChart(dataChart) {
    var arrData = [['Country', 'Doanh Thu']];
    dataChart.forEach(element => {
      var item = [];
      item.push(element.shipCountry);
      item.push(element.doanhThu);
      arrData.push(item);
    });
    var data = google.visualization.arrayToDataTable(arrData);
    
    var options = {
      title: 'Biểu đồ doanh thu theo quốc gia'
      
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(data, options);
  }

}
