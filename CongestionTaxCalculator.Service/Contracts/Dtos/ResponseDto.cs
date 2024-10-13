using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.Dtos;

public record ResponseDto(string Date, int Tax);
