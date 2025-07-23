import { ConfirmationService, MessageService } from 'primeng/api';
import { Observable } from 'rxjs';

export function confirmarAccionObservable<T>(
    confirmationService: ConfirmationService,
    messageService: MessageService,
    opciones: {
        mensaje: string;
        titulo?: string;
        entidad?: string;
        onConfirmar: () => Observable<{ success: boolean; message?: string; errors?: any }>;
        exito?: string;
        fallo?: string;
        cancelado?: string;
        onSuccess?: () => void;
        onErrorValidacion?: (errors: any) => void;
    }
) {
    confirmationService.confirm({
        message: opciones.mensaje,
        header: opciones.titulo ?? 'Confirmación',
        icon: 'pi pi-exclamation-triangle',
        acceptLabel: 'Sí',
        rejectLabel: 'No',
        accept: () => {
            opciones.onConfirmar().subscribe({
                next: (res) => {
                    if (res.success) {
                        messageService.add({
                            severity: 'success',
                            summary: 'Éxito',
                            detail: opciones.exito ?? res.message ?? 'Operación realizada',
                            life: 3000
                        });
                        opciones.onSuccess?.();
                    } else {
                        opciones.onErrorValidacion?.(res.errors ?? null);
                        messageService.add({
                            severity: 'warn',
                            summary: 'No completado',
                            detail: opciones.fallo ?? res.message ?? 'No se completó la acción',
                            life: 3000
                        });
                    }
                },
                error: (err) => {
                    console.error(err);

                    const backendErrors = err?.error?.errors;
                    if (backendErrors && typeof backendErrors === 'object') {
                        opciones.onErrorValidacion?.(backendErrors);
                    }

                    messageService.add({
                        severity: 'error',
                        summary: 'Error',
                        detail: opciones.fallo ?? 'Ocurrió un error inesperado',
                        life: 3000
                    });
                }

            });
        },
        reject: () => {
            messageService.add({
                severity: 'info',
                summary: 'Cancelado',
                detail: opciones.cancelado ?? 'Acción cancelada',
                life: 2000
            });
        }
    });
}
