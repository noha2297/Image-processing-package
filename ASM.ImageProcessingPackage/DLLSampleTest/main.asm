INCLUDE Irvine32.inc
.data
	RGB_Arr_ptr dword ?
	Arr_Width dword ?
	Arr_Hight dword ?

	Window_Arr_ptr dword ?
	Window_Width dword ?
	Window_Size dword ?
	
	
	median_val byte ?
	

.code

Median_Filter PROC  rgb_arr:ptr byte , rgb_arr_width : dword , rgb_arr_hight :dword , windo_arr:ptr byte  , windo_width : dword 
pushad

	mov eax , rgb_arr
	mov RGB_Arr_ptr , eax

	mov eax , rgb_arr_width
	mov Arr_Width , eax

	mov eax , rgb_arr_hight
	mov Arr_Hight , eax

	mov eax , windo_arr
	mov Window_Arr_ptr , eax

	mov eax , windo_width
	mov Window_Width , eax
	
	mov eax , Window_Width
	mul Window_Width
	mov Window_Size , eax

	mov esi, RGB_Arr_ptr  
	mov ecx , Arr_Hight
	;sub ecx , Window_Width  ;; q?? window_width wla widow_width -1
	;add ecx , 1


	l1:
		push ecx
		mov ecx , arr_width
		sub ecx , Window_Width
		add ecx , 1
		l2:
			call Window
			call Sort
			call find_median
			inc esi

		loop l2

		pop ecx
		

	loop l1
popad
ret
Median_Filter ENDP


Window PROC  uses esi ecx

	mov edi, esi
	mov ebx, Window_Arr_ptr

	mov ecx, Window_Width

	l1:
		
		mov eax, ecx
		mov ecx , Window_Width

		l2:
		
			mov dl, [esi]
			mov [ebx], dl
			inc esi
			inc ebx

		loop l2

		add edi, Arr_Width
		mov esi , edi
		mov ecx, eax

	loop l1

	ret
Window ENDP



Sort PROC  uses esi ecx
	pushad
	mov edi, Window_Arr_ptr
	mov esi, Window_Arr_ptr

	
	mov ecx , Window_Size
	sub ecx ,1
	
	l1:
		mov dl, [edi]
		mov eax , ecx

		inc esi		

		l2:
			cmp [esi],dl
			jae skip
			
			mov bl , dl
			xchg bl, [esi]
			mov dl, bl
			mov [edi], dl
			skip:
			inc esi
		loop l2

		mov ecx , eax
		inc edi
		mov esi, edi

	loop l1

	popad
ret
Sort ENDP


Find_Median PROC uses esi ecx


	mov eax , Window_Size
	mov edx , 0
	mov ebx , 2
	;cdq
	div ebx
;	add eax , 1  ; reminder of edx always 1

	mov edi , Window_Arr_ptr
	add edi , eax

	mov bl , [edi]
	mov median_val , bl

	
	mov eax , Window_Width
	mov edx , 0
	mov ebx , 2
	;cdq
	div ebx
	;add eax , 1 

	mov edi , esi
	add edi , eax

	mov ecx , eax
	l1:
		add edi , Arr_Width
	loop l1
	
	mov al , median_val
	mov [edi], al




ret
Find_Median ENDP



Brightness PROC  Arr_ptr : ptr byte, Arr_size :dword , brightness_value : dword
pushad
mov esi, Arr_ptr
mov ecx, Arr_size

l1:
mov ebx , 0
mov bl,[esi]
add ebx, brightness_value
mov edx,255
cmp ebx,edx
jg reset
mov edx,0
cmp ebx,edx
jl reset2

mov [esi],dl
jmp skip
reset2 :
mov dl , 0
mov [esi],dl
jmp skip
reset:
mov ebx, 255
mov [esi],bl

skip:
inc esi

loop l1

popad
ret
Brightness ENDP

GrayScale PROC Arr_ptr : ptr byte , SizeOf_RGB_Arr :dword
pushad
mov esi,Arr_ptr
mov ecx, SizeOf_RGB_Arr
l1:
mov ebx , 0
mov bl,[esi]
mov [esi+1],bl
mov [esi+2],bl
add esi,3
sub ecx, 2
loop l1

popad
ret
GrayScale ENDP

GrayScale2 PROC Arr_ptr : ptr byte , SizeOf_RGB_Arr :dword
pushad

mov esi,Arr_ptr
mov ecx, SizeOf_RGB_Arr
mov ebx , 3
l1:
mov eax , 0
mov edx , 0

mov dl, byte ptr[esi]
add eax , edx

mov dl ,byte ptr [esi+1]
add eax , edx

mov dl , byte ptr[esi+2]
add eax , edx

mov edx , 0
div ebx

mov [esi],al
mov [esi+1],al
mov [esi+2],al

add esi,3
sub ecx, 2
loop l1

popad
ret
GrayScale2 ENDP

Inverse PROC Arr_ptr : ptr byte, Arr_size :dword

mov esi ,Arr_ptr
mov ecx, Arr_size
l1:
mov eax , 0 
mov bl , 255
mov al,[esi]
sub bl , al
mov [esi],bl
inc esi
loop l1

ret
Inverse  ENDP

Subtraction PROC Arr1_ptr : ptr byte, Arr1_size :dword , Arr2_ptr : ptr byte, Arr2_size :dword

mov esi ,Arr1_ptr
mov edi , Arr2_ptr
mov ecx, Arr1_size

l1:
mov ebx , 0
mov bl , [edi]
sub [esi], bl
inc esi
inc edi
loop l1

ret
Subtraction ENDP

Addition PROC Arr1_ptr : ptr byte, Arr1_size :dword , Arr2_ptr : ptr byte, Arr2_size :dword

mov esi ,Arr1_ptr
mov edi , Arr2_ptr
mov ecx, Arr1_size

l1:
mov ebx , 0
mov bl , [edi]
add [esi], bl
inc esi
inc edi
loop l1

ret
Addition ENDP

; DllMain is required for any DLL
DllMain PROC hInstance:DWORD, fdwReason:DWORD, lpReserved:DWORD
mov eax, 1 ; Return true to caller.
ret
DllMain ENDP
END DllMain