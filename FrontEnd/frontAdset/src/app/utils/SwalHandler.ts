import Swal from 'sweetalert2';
import { Router } from '@angular/router';
export class SwalHandler {


  constructor(private router: Router) {}

  static showSucessoRedirecionamento(router: Router, title: string, message: string, redirectUrl: string) {
    Swal.fire({
      icon: 'success',
      title: title,
      text: message,
      timerProgressBar: true,
      showConfirmButton: false,
      willClose: () => {
        router.navigate([redirectUrl]);
      }
    });
  }

    static showFalhaRedirecionamento(router: Router, title: string, message: string, redirectUrl: string) {
    Swal.fire({
      icon: 'warning',
      title: title,
      text: message,
      timerProgressBar: true,
      showConfirmButton: false,
      willClose: () => {
        router.navigate([redirectUrl]);
      }
    });
  }

  static showAtencaoHtml(titulo: string, mensagemHtml: string) {
  Swal.fire({
    icon: 'warning',
    title: titulo,
    html: mensagemHtml,
    confirmButtonColor: '#3085d6'
  });
}

  static showSucesso(title: string, message: string) {
    Swal.fire({
      icon: 'success',
      title: title,
      text: message,
      timer: 2000,
      timerProgressBar: true,
      showConfirmButton: false
    });
  }

  static SwalSucessoReload(title: string, message: string) {
  Swal.fire({
    icon: 'success',
    title: title,
    text: message,
    timer: 2000,
    timerProgressBar: true,
    showConfirmButton: false,
    didClose: () => {
      window.location.reload();
    }
  });
}

   static showAtencao(title: string, message: string) {
    Swal.fire({
      icon: 'warning',
      title: title,
      text: message,
      timer: 2000,
      timerProgressBar: true,
      showConfirmButton: false
    });
  }
  /**
   * Exibe um alerta de erro (falha).
   * @param title Título do alerta.
   * @param message Mensagem do alerta.
   */
  static showFalha(title: string, message: string) {
    Swal.fire({
      icon: 'error',
      title: title,
      text: message,
      confirmButtonText: 'OK',
      confirmButtonColor: '#d33'
    });
  }

  /**
   * Exibe um alerta de sucesso e redireciona após alguns segundos.
   * @param title Título do alerta.
   * @param message Mensagem do alerta.
   * @param redirectUrl URL para redirecionamento após o alerta.
   * @param delay Tempo de exibição em milissegundos (padrão: 2000 ms).
   */


  /**
   * Exibe um alerta de confirmação com ação callback.
   * @param title Título do alerta.
   * @param message Mensagem do alerta.
   * @param onConfirm Função a ser executada se o usuário confirmar.
   */
  static showConfirmacao(title: string, message: string, onConfirm: () => void) {
    Swal.fire({
      title: title,
      text: message,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Sim',
      cancelButtonText: 'Cancelar',
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33'
    }).then(result => {
      if (result.isConfirmed) {
        onConfirm();
      }
    });
  }
}
