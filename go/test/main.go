package test

import (
	"fmt"
    "time"
    "math/rand"
	"strconv"
)

var count =0


func process(n int){
    for i := 0; i<10;i++{
        time.Sleep(time.Duration(rand.Int31n(2))*time.Second)
        temp := count
        temp++
        time.Sleep(time.Duration(rand.Int31n(2))*time.Second)
        count = temp
    }
    fmt.Println("count after i="+ strconv.Itoa(n)+" Count", strconv.Itoa(count))
}

func main() {
    for i := 1 ; i<4;i++{
        go process(i)
    }
    time.Sleep(25* time.Second)
    fmt.Println("Final Count", count)
}