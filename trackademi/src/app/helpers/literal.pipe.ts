import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'literal',
    standalone: true
})
export class LiteralPipe implements PipeTransform {
    transform(nota: number | null | undefined): string {
        if (nota == null || isNaN(nota)) return '—';

        if (nota >= 90) return 'A';
        if (nota >= 80) return 'B';
        if (nota >= 70) return 'C';
        return 'F';
    }
}
